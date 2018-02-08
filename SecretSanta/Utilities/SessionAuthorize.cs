using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Autofac.Integration.WebApi;
using SecretSanta.Controllers;
using SecretSanta.Service.IServices;

namespace SecretSanta.Utilities
{
    public class SessionAuthorizeAttribute : ActionFilterAttribute, IAutofacActionFilter
    {
        private readonly IAccountService _accountService;

        public SessionAuthorizeAttribute(IAccountService accountService)
        {
            this._accountService = accountService;
        }

        public override async Task OnActionExecutingAsync(HttpActionContext actionContext,
            CancellationToken cancellationToken)
        {
            if (SkipAuthorization(actionContext))
            {
                return;
            }

            IEnumerable<string> authValues;
            if (!actionContext.Request.Headers.TryGetValues("Authorization", out authValues))
                throw new HttpResponseException(HttpStatusCode.Unauthorized);

            var authToken = authValues.First().Substring(7);
            var userSession = _accountService.ReValidateSession(authToken);

            if (userSession == null)
                throw new HttpResponseException(HttpStatusCode.Unauthorized);

            if (actionContext.ControllerContext.Controller.GetType() == typeof(GroupController))
            {
                ((GroupController)actionContext.ControllerContext.Controller)
                    .SetCurrentUserId(userSession.UserId);
            }
            else
            {
                ((UserController)actionContext.ControllerContext.Controller)
                    .SetCurrentUser(userSession.UserId, userSession.User.UserName);
            }

            await base.OnActionExecutingAsync(actionContext, cancellationToken);
        }

        private static bool SkipAuthorization(HttpActionContext actionContext)
        {
            return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                   || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
        }
    }
}