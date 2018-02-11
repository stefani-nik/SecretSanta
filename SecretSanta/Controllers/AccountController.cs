using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using SecretSanta.Service.IServices;
using Microsoft.AspNet.Identity;
using SecretSanta.Dtos;
using Microsoft.Owin.Testing;

namespace SecretSanta.Controllers
{
    [RoutePrefix("api")]
    public class AccountController : ApiController
    {
        private readonly IAccountService _accountsService;
        private string _currentUserUsername;
        private string _currentUserId;

        public AccountController(IAccountService accountsService)
        {
            this._accountsService = accountsService;
        }

        public void SetCurrentUser(string id, string username)
        {
            this._currentUserId = id;
            this._currentUserUsername = username;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Login(LoginDto model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return this.BadRequest("Username or password is not correct.");
            }

            var testServer = TestServer.Create<Startup>();
            var requestParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", model.Username),
                new KeyValuePair<string, string>("password", model.Password)
            };
            var requestParamsFormUrlEncoded = new FormUrlEncodedContent(requestParams);

            var tokenServiceResponse = await testServer.HttpClient.PostAsync(
                Startup.TokenEndpointPath, requestParamsFormUrlEncoded);

            if (tokenServiceResponse.IsSuccessStatusCode)
            {
                var responseString = await tokenServiceResponse.Content.ReadAsStringAsync();
                var jsSerializer = new JavaScriptSerializer();
                var responseData =
                    jsSerializer.Deserialize<Dictionary<string, string>>(responseString);
                var authToken = responseData["access_token"];
                var username = responseData["userName"];

                try
                {
                    this._accountsService.CreateUserSession(username, authToken);
                }
               catch (Exception)
               {
                   
                    return NotFound();
               }

                this._accountsService.DeleteExpiredSessions();
            }
            else
            {
                return this.ResponseMessage(tokenServiceResponse);
            }


            return this.ResponseMessage(tokenServiceResponse);
        }

        [HttpDelete]
        [Route("logout")]
        public IHttpActionResult Logout()
        {
            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie, DefaultAuthenticationTypes.ExternalBearer);

            try
            {
                var isInvalidated = this._accountsService.InvalidateUserSession();
                if (!isInvalidated)
                {
                    return this.NotFound();
                }
            }
            catch (ArgumentNullException)
            {
                return this.BadRequest();
            }

            return this.Ok(new {message =  "Logout successful"});
        }



    }
}
