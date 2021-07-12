using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Text;
using System.Web;
using System.Web.Http;

//[assembly: OwinStartup(typeof(Angular_WebAPI.Startup))]
namespace Angular_WebAPI
{
    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            var myProvider = new MyAuthProvider();
            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(10),
                Provider = myProvider
            };
            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            //app.UseOAuthBearerTokens(options);

            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
        }

        //public void Configuration(IAppBuilder app)
        //{
        //    app.UseJwtBearerAuthentication(
        //        new JwtBearerAuthenticationOptions
        //        {
        //            AuthenticationMode = AuthenticationMode.Active,
        //            TokenValidationParameters = new TokenValidationParameters()
        //            {
        //                ValidateIssuer = true,
        //                ValidateAudience = true,
        //                ValidateIssuerSigningKey = true,
        //                ValidIssuer = "http://mysite.com", //some string, normally web url,  
        //                ValidAudience = "http://mysite.com",
        //                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my_secret_key_12345"))
        //            }
        //        });
        //}


    }
    }

//Second try:

