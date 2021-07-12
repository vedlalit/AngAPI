using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Claims;
using Angular_WebAPI.Models;


namespace Angular_WebAPI.Controllers
{
    public class JWTController : ApiController
    {
        //EmployeeDBEntities obj = new EmployeeDBEntities();
        //var userdata = obj.EF_UserLogin(context.UserName, context.Password).FirstOrDefault();
        //[HttpGet]
        public Object GetToken()
        {
            string key = "my_secret_key_12345"; //Secret key which will be used later during validation    
            var issuer = "http://mysite.com";  //normally this will be your site URL    

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Create a List of Claims, Keep claims name short    
            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("valid", "1"));
            permClaims.Add(new Claim("userid", "1"));
            permClaims.Add(new Claim("name", "bilal"));

            //var identityClaims = (ClaimsIdentity)User.Identity;
            //IEnumerable<Claim> claims = identityClaims.Claims;
            //UserLogin model = new UserLogin()
            //{
            //    UserName = identityClaims.FindFirst("UserName").Value,
            //    UserEmail = identityClaims.FindFirst("UserEmail").Value,
            //    UserPassword = identityClaims.FindFirst("UserPassword").Value,
            //    UserRole = identityClaims.FindFirst("UserRole").Value,
            //    //LastName = identityClaims.FindFirst("LastName").Va    lue,
            //    //LoggedOn = identityClaims.FindFirst("LoggedOn").Value
            //};

            //Create Security Token object by giving required parameters    
            var token = new JwtSecurityToken(issuer, //Issure    
                            issuer,  //Audience    
                            permClaims,
                            expires: DateTime.Now.AddDays(1),
                            signingCredentials: credentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            return new { data = jwt_token };

        }
        [HttpPost]
        public String GetName1()
        {
            if (User.Identity.IsAuthenticated)
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                }
                return "Valid";
            }
            else
            {
                return "Invalid";
            }
        }
        [Authorize]
        [HttpPost]
        public Object GetName2()
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var name = claims.Where(p => p.Type == "name").FirstOrDefault()?.Value;
                return new
                {
                    data = name
                };

            }
            return null;
        }
    }

}
