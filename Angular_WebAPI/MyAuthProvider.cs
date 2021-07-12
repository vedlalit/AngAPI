using Microsoft.Owin.Security.OAuth;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Angular_WebAPI.Models;
using System.Security.Principal;
using System.Collections.Generic;

namespace Angular_WebAPI
{
    public class MyAuthProvider : OAuthAuthorizationServerProvider
    {

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            


            EmployeeDBEntities obj = new EmployeeDBEntities();
            var userdata = obj.EF_UserLogin(context.UserName, context.Password).FirstOrDefault();
            if (userdata == null)
            {

                context.SetError("invalid_grant", "Provided username and password is incorrect");
                context.Rejected();

            }
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

          
            identity.AddClaim(new Claim("UserRole", userdata.UserRole));
            identity.AddClaim(new Claim("UserName", userdata.UserName));
            identity.AddClaim(new Claim("UserPassword", userdata.UserPassword));
            identity.AddClaim(new Claim("UserEmail", userdata.UserEmail));
            context.Validated(identity);
        }

        //    private static bool ValidateToken(string token, out string username)
        //    {
        //        username = null;

        //        var simplePrinciple = JWTManager.GetPrincipal(token);
        //        var identity = simplePrinciple.Identity as ClaimsIdentity;

        //        if (identity == null)
        //            return false;

        //        if (!identity.IsAuthenticated)
        //            return false;

        //        var usernameClaim = identity.FindFirst(ClaimTypes.Name);
        //        username = usernameClaim?.Value;

        //        if (string.IsNullOrEmpty(username))
        //            return false;

        //        // More validate to check whether username exists in system

        //        return true;
        //    }

        //    protected Task<IPrincipal> AuthenticateJwtToken(string token)
        //    {
        //        string username;

        //        if (ValidateToken(token, out username))
        //        {
        //            // based on username to get more information from database 
        //            // in order to build local identity
        //            var claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Name, username)
        //        // Add more claims if needed: Roles, ...
        //    };

        //            var identity = new ClaimsIdentity(claims, "Jwt");
        //            IPrincipal user = new ClaimsPrincipal(identity);

        //            return Task.FromResult(user);
        //        }

        //        return Task.FromResult<IPrincipal>(null);
    }

    //}

}