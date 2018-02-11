using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using SecretSanta.Dtos;
using SecretSanta.Models;
using SecretSanta.Service.IServices;

namespace SecretSanta.Controllers
{
    [RoutePrefix("api/groups")]
    public class GroupController : ApiController
    {
        private readonly IGroupService _groupService;
        private readonly IInvitationService _invitationService;
        private readonly IUserService _userService;
        private readonly IConnectionService _connectionService;
        private string _currentUserId;

        public GroupController(IGroupService groupService,
            IUserService userService,
            IInvitationService invitationService,
            IConnectionService connectionService)
        {
            this._groupService = groupService;
            this._userService = userService;
            this._invitationService = invitationService;
            this._connectionService = connectionService;
        }

        public void SetCurrentUserId(string id)
        {
            this._currentUserId = id;
        }

        [HttpGet]
        [Route("{groupName}")]
        public IHttpActionResult GetGroup([FromUri] string groupName)
        {
            if (string.IsNullOrEmpty(groupName))
            {
                return BadRequest();
            }

            var group = this._groupService.GetGroupByName(groupName);

            if (group == null)
            {
                return NotFound();
            }

            var result = new GroupDto(group.Name, group.Creator.DisplayName);

            if (group.Creator.Id == this._currentUserId)
            {
                var members = group.Members
                    .Select(u => new UserProfileDto(u.UserName, u.DisplayName))
                    .ToList();
                result.Members = members;
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateGroup([FromBody]CreateGroupDto groupModel)
        {
            if (groupModel == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentUser = this._userService.GetUserById(this._currentUserId);

            try
            {
                var group = new Group
                {
                    Name = groupModel.Name,
                    Creator = currentUser
                };
                group.Members.Add(currentUser);
           
                this._groupService.CreateGroup(group);
          
                var model = new GroupDto(group.Name, group.Creator.DisplayName);

                return Content(HttpStatusCode.Created, model);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.Conflict, "Groupname should be unique.");
            }
        }

        [HttpGet]
        [Route("{groupName}/members")]
        public IHttpActionResult GetGroupMembers(string groupName)
        {
            if (string.IsNullOrEmpty(groupName))
            {
                return BadRequest();
            }

            var group = this._groupService.GetGroupByName(groupName);

            if (group == null)
            {
                return NotFound();
            }

            if (group.Creator.Id != this._currentUserId)
            {
                return Content(HttpStatusCode.Forbidden, "Only the owner of the group can see the members!");
            }

            var members = group.Members
                .Select(p => new UserProfileDto(p.UserName, p.DisplayName));

            return Ok(members);
        }

        [HttpPost]
        [Route("{groupName}/members")]
        public IHttpActionResult AddMember([FromUri]string groupName, [FromBody]UserProfileDto userModel)
        {

            if (string.IsNullOrEmpty(groupName) || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = this._userService.GetUserByUsername(userModel.UserName);
            if (user == null)
            {
                return NotFound();
            }

            var hasRequest = this._invitationService.IsUserInvited(groupName, user.Id);
            if (!hasRequest)
            {
                return Content(HttpStatusCode.Forbidden, "You do not have request for this group.");
            }

            this._groupService.AddMember(groupName, user);
            this._invitationService.AcceptInvitation(groupName, user.Id);

            return Content(HttpStatusCode.Created, "You are now member of this group.");
        }

        [HttpDelete]
        [Route("{groupName}/members/{username}")]
        public IHttpActionResult DeleteParticipant([FromUri]string groupName, [FromUri] string username)
        {
            if (string.IsNullOrEmpty(groupName) || string.IsNullOrEmpty(username))
            {
                return BadRequest();
            }

            var user = this._userService.GetUserByUsername(username);
            if (user == null)
            {
                return NotFound();
            }

            var group = this._groupService.GetGroupByName(groupName);
            if (group == null)
            {
                return NotFound();
            }

            if (group.Creator.Id != this._currentUserId)
            {
                return Content(HttpStatusCode.Forbidden, "Only the owner of group can remove members!");
            }

            if (@group.State == ConnectionsState.Connected)
            {
                return Content(HttpStatusCode.Forbidden,
                    "You cannot remove user because the process of connections is already started!");
            }

            this._groupService.RemoveUserFromGroup(group.GroupId, user.Id);
            return this.Ok(new { message = "Member was deleted." });
        }

        [HttpPut]
        [Route("{groupName}/connections")]
        public IHttpActionResult ConnectPeople([FromUri] string groupName)
        {
            if (string.IsNullOrEmpty(groupName))
            {
                return BadRequest();
            }

            var group = this._groupService.GetGroupByName(groupName);

            if (group == null)
            {
                return NotFound();
            }

            if (group.Creator.Id != this._currentUserId)
            {
                return Content(HttpStatusCode.Forbidden, "Only the owner of group can start the process of connection!");
            }

            var members = this._groupService.GetMembers(group.GroupId);
            if (members.Count < 2)
            {
                return Content(HttpStatusCode.PreconditionFailed, "The process of connection cannot be started because the group has only one member!");
            }

            if (group.State == ConnectionsState.Connected)
            {
                return Content(HttpStatusCode.PreconditionFailed, "The process of connection has already been started");
            }

            this._connectionService.CreateConnections(group.GroupId);
            this._groupService.ChangeState(group.GroupId);

            return Content(HttpStatusCode.Created, "The process of connection was started!");
        }
    }
}
