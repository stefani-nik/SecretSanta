using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SecretSanta.Dtos;
using SecretSanta.Models;
using SecretSanta.Service.IServices;

namespace SecretSanta.Controllers
{
    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        private ApplicationUserManager _userManager;
        private readonly IUserService _usersService;
        private readonly IGroupService _groupsService;
        private readonly IInvitationService _invitationService;
        private readonly IConnectionService _connectionsService;
        private string _currentUserUsername;
        private string _currentUserId;



        public UserController(IUserService usersService,
            IGroupService groupsService,
            IInvitationService invitationService,
            IConnectionService connectionsService)
        {
            this._usersService = usersService;
            this._groupsService = groupsService;
            this._invitationService = invitationService;
            this._connectionsService = connectionsService;
     
        }


        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public void SetCurrentUser(string id, string username)
        {
            this._currentUserId = id;
            this._currentUserUsername = username;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Register(RegisterDto model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser
            {
                UserName = model.Username,
                DisplayName = model.DisplayName
            };

            IdentityResult result = await UserManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var resultModel = new UserProfileDto(user.UserName, user.DisplayName);

                return Content(HttpStatusCode.Created, resultModel);
            }

            return Content(HttpStatusCode.Conflict,  result.Errors);
            

        }

        [HttpGet]
        [Route("{username}")]
        public IHttpActionResult GetProfile([FromUri] string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest();
            }

            var user = this._usersService.GetUserByUsername(username);

            if (user == null)
            {
                return Content(HttpStatusCode.NotFound, "There is no user with that username.");
            }

            var model = new UserProfileDto(user.UserName, user.DisplayName);

            return Ok(model);
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllUsers([FromUri]GetUsersCriteria criteria)
        {
            if (criteria.Order.ToLower() != "asc" && criteria.Order.ToLower() != "desc")
            {
                return BadRequest();
            }

            var users = this._usersService
                .GetPageOfUsers(criteria.Page, criteria.Order, criteria.Search)
                .Select(u => new UserProfileDto(u.UserName, u.DisplayName))
                .ToList();

            return Ok(users);
        }

        [HttpGet]
        [Route("{username}/groups")]
        public IHttpActionResult GetUserGroups(string username, int page)
        {

            // TODO : You can only get your own groups
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest();
            }

            try
            {
                var groups = this._usersService.GetUserGroups(username, page)
                     .Select(g => new { GroupName = g.Name });

                return Ok(groups);
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("{username}/groups/{groupName}/connections")]
        public IHttpActionResult GetUserConnectionInGroup([FromUri] string username, [FromUri] string groupName)
        {

            // Todo : you can only get your own connection
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(groupName))
            {
                return BadRequest();
            }

            var connection = this._connectionsService.GetConnectionInGroup(username, groupName);

            if (connection == null)
            {
                return Content(HttpStatusCode.Forbidden, "The connection process has not been started yet.");
            }

            var result = new UserProfileDto(connection.Receiver.UserName, connection.Receiver.DisplayName);

            return Ok(result);
        }

        [HttpGet]
        [Route("{username}/invitations")]
        public IHttpActionResult GetUsersInvitations([FromUri] string username, [FromUri]GetUsersCriteria criteria)
        {
            if (string.IsNullOrEmpty(username) ||
                (criteria.Order.ToLower() != "asc" && criteria.Order.ToLower() != "desc"))
            {
                return BadRequest();
            }

            var user = this._usersService.GetUserByUsername(username);

            if (user == null)
            {
                return NotFound();
            }

            if (this._currentUserId != user.Id)
            {
                return Content(HttpStatusCode.Forbidden, "You can see only your requests.");
            }

            var invitations = this._invitationService.GetPageOfPendingInvitations(this._currentUserId,criteria.Page, criteria.Order)
                .Select(r => new InvitationDto(r.InvitationId, r.Group.Name, r.Group.Creator.DisplayName, r.Date));

            return Ok(invitations);
        }

        [HttpPost]
        [Route("{username}/invitations")]
        public IHttpActionResult SendInvitation([FromUri] string username, [FromBody]InvitationDto invitation)
        {

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(invitation.GroupName))
            {
                return BadRequest();
            }

            var user = this._usersService.GetUserByUsername(username);
            var group = this._groupsService.GetGroupByName(invitation.GroupName);

            if (user == null)
            {
                return Content(HttpStatusCode.NotFound, "There is no such user in the database");
            }

            if (group == null)
            {
                return Content(HttpStatusCode.NotFound, "There is no such group in the database");
            }

            if (this._currentUserUsername != group.Creator.UserName)
            {
                return Content(HttpStatusCode.Forbidden, "You cannot send invitations for this group.");
            }

            if (this._currentUserUsername == username)
            {
                return Content(HttpStatusCode.Forbidden, "You cannot send invitation to yourself.");
            }

            var hasRequest = this._invitationService.IsUserInvited(invitation.GroupName, user.Id);

            if (hasRequest)
            {
                return Content(HttpStatusCode.Conflict, "You have already sent request to this user.");
            }

            var invitationToSend = new Invitation
            {
                Date = invitation.Date,
                Receiver = user,
                Group = group
            };

            this._invitationService.CreateInvittation(invitationToSend);
            var result = new InvitationDto(invitationToSend.InvitationId, invitationToSend.Group.Name, group.Creator.UserName,
                invitationToSend.Date);

            return Content(HttpStatusCode.Created, result);
        }

        [HttpDelete]
        [Route("{username}/invitations/{id}")]
        public IHttpActionResult DeleteInvitation([FromUri] string username, [FromUri] string id)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            var user = this._usersService.GetUserByUsername(username);
            if (user == null)
            {
                return NotFound();
            }

            if (user.UserName != this._currentUserUsername)
            {
                return Content(HttpStatusCode.Forbidden, "You can decline only your invitations.");
            }

            var invitation = user.PendingInvitations.FirstOrDefault(i => i.InvitationId == int.Parse(id));

            if (invitation == null)
            {
                return NotFound();
            }

            this._invitationService.CancelInvitation(invitation.Group.GroupId, invitation.Receiver.Id);
            return Content(HttpStatusCode.NoContent, "The invitation was declined!");
        }
    }
}