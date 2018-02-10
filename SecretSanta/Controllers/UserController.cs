using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
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
            else
            {
                return Content(HttpStatusCode.Conflict,  result.Errors);
            }

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
                return NotFound();
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
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest();
            }

            try
            {
                var groups = this._groupsService.GetPageOfGroups(username, page)
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
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(groupName))
            {
                return BadRequest();
            }

            var connection = this._connectionsService.GetConnectionInGroup(username, groupName);
            if (connection == null)
            {
                return NotFound();
            }

            var result = new UserProfileDto(connection.Receiver.UserName, connection.Receiver.DisplayName);

            return Ok(result);
        }

        [HttpGet]
        [Route("{username}/invitations")]
        public IHttpActionResult GetAllRequests([FromUri] string username, [FromUri]GetUsersCriteria criteria)
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

            var requests = this._invitationService.GetPageOfPendingInvitations(this._currentUserId,criteria.Page, criteria.Order)
                .Select(r => new InvitationDto(r.InvitationId, r.Group.Name, r.Group.Creator.DisplayName, r.Date));

            return Ok(requests);
        }

        [HttpPost]
        [Route("{username}/invitations")]
        public IHttpActionResult SendRequest([FromUri] string username, [FromBody]InvitationDto request)
        {

            // TODO: The owner cannot send requests to himself
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(request.GroupName))
            {
                return BadRequest();
            }

            var user = this._usersService.GetUserByUsername(username);
            var group = this._groupsService.GetGroupByName(request.GroupName);
            if (user == null || group.Name == null)
            {
                return NotFound();
            }

            if (this._currentUserUsername != group.Creator.UserName)
            {
                return Content(HttpStatusCode.Forbidden, "You cannot send requests for this group.");
            }

            var hasRequest = this._invitationService.IsUserInvited(request.GroupName, user.Id);
            if (hasRequest)
            {
                return Content(HttpStatusCode.Conflict, "You have already sent request to this user.");
            }

            var requestToSend = new Invitation
            {
                Date = request.Date,
                Receiver = user,
                Group = group
            };

            this._invitationService.CreateInvittation(requestToSend);
            var result = new InvitationDto(requestToSend.InvitationId, requestToSend.Group.Name, group.Creator.UserName,
                requestToSend.Date);

            return Content(HttpStatusCode.Created, result);
        }

        [HttpDelete]
        [Route("{username}/invitations/{id}")]
        public IHttpActionResult DeleteRequest([FromUri] string username, [FromUri] string id)
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
                return Content(HttpStatusCode.Forbidden, "You can delete only your requests.");
            }

            var request = user.PendingInvitations.FirstOrDefault(i => i.InvitationId == int.Parse(id));
            if (request == null)
            {
                return NotFound();
            }

            this._invitationService.CancelInvitation(request.Group.GroupId, request.Receiver.Id);
            return Content(HttpStatusCode.NoContent, "The request was deleted!");
        }
    }
}