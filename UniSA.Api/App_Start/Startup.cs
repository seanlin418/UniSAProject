using System;
using Owin;
using System.Web.Http;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Microsoft.Owin.Security.OAuth;
using UniSA.Api.Services;
using Microsoft.Owin;
using UniSA.Services;

[assembly: OwinStartup(typeof(UniSA.Api.Startup))]

namespace UniSA.Api
{
    public class Startup
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            //OWIN Middleware - Configure WebApi
            ConfigureWebApi(app);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                ApplicationCanDisplayErrors = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = new SimpleAuthorizationServerProvider(),
                RefreshTokenProvider = new SimpleRefreshTokenProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);
        }

        public void ConfigureWebApi(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            //Configure OAuth
            ConfigureOAuth(app);

            WebApiConfig.Register(config);

            //Sean Lin: UseCors must be put after the configuration of OAuth to disable the auto padding (remove) 
            //of Access-Control-Allow-Origin and Access-Control-Allow-Credential from the request/response header.
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);

            app.UseNinjectMiddleware(() => NinjectConfig.CreateKernel.Value);
            app.UseNinjectWebApi(config);
        }
    }
}