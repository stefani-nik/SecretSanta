using System;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using SecretSanta.Data;

namespace SecretSanta
{
    public class Startup
    {
        private const string PublicClientId = "self";

        public const string TokenEndpointPath = "/api/token";

        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(SecretSantaContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // Configure the application for OAuth based flow
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString(TokenEndpointPath),
                Provider = new OAuthAuthorizationServerProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);
        }
    }
}