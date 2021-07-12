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
using Angular_WebAPI.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Web.Http.Description;

//No adds

//[AllowAnonymous]
//public string Get(string username, string password)
//{
//    if (CheckUser(username, password))
//    {
//        return JwtManager.GenerateToken(username);
//    }

//    throw new HttpResponseException(HttpStatusCode.Unauthorized);
//}

//public bool CheckUser(string username, string password)
//{
//    EmployeeDBEntities obj = new EmployeeDBEntities();
//    // should check in the database q
//    return true;
//}

namespace Angular_WebAPI.Controllers
{
    
   public class LoginController : ApiController
    {
        private EmployeeDBEntities db = new EmployeeDBEntities();

        [HttpGet]

        [Route("api/GetUserClaims")]
        public UserLogin GetUserClaims()
        {
            var identityClaims = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identityClaims.Claims;
            UserLogin model = new UserLogin()
            {
                UserName = identityClaims.FindFirst("UserName").Value,
                UserEmail = identityClaims.FindFirst("UserEmail").Value,
                UserPassword = identityClaims.FindFirst("UserPassword").Value,
                UserRole = identityClaims.FindFirst("UserRole").Value,
                //LastName = identityClaims.FindFirst("LastName").Va    lue,
                //LoggedOn = identityClaims.FindFirst("LoggedOn").Value
            };
           
            return model;
        }

        private bool UserExists(string name)
        {
            return db.UserLogins.Count(e => e.UserName == name) > 0;
        }

      

        
        [Route("api/PutUserEmail")]
    

        public IHttpActionResult PutUser(string name,UserLogin userLogin)
        {

            //UserLogin userLogin = new UserLogin();

            //userLogin.UserEmail = email;
           
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            if (name != userLogin.UserName)
            {
                return BadRequest();
            }

            db.Entry(userLogin).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(name))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.Accepted);
        }

        //public ActionResult UpdateEmployee(string Email)
        //{
        //    EmployeeDBEntities repo = new EmployeeDBEntities() ;

        //    Models.UserLogin usermodel = new Models.UserLogin();
        //   usermodel.UserEmail

        //    return View(EmployeeModel);
        //}

        //[HttpPost]
        //public ActionResult UpdateUser(Models.UserLogin userLogin)
        //{
        //    repo = new Employee_DAL.EmpRepo();
        //    var EmpDal = repo.ViewCustomerDetails(Convert.ToInt32(Request.QueryString["EmpId"]));
        //    //Models.Employee custModel = new Models.Customer();
        //    EmpDal.EmployeeId = EmpModel.EmployeeId;
        //    EmpDal.FirstName = EmpModel.FirstName;
        //    EmpDal.LastName = EmpModel.LastName;
        //    EmpDal.Designation = EmpModel.Designation;
        //    EmpDal.Country = EmpModel.Country;
        //    //EmpDal.DeliveryAddress = EmpModel.DeliveryAddress;
        //    //EmpDal.EmailId = EmpModel.EmailId;
        //    //EmpDal.Password = EmpModel.Password;
        //    if (repo.UpdateEmployee(EmpDal))
        //    {
        //        ViewBag.Message = "updation done!";
        //    }
        //    else
        //    {
        //        ViewBag.Message = "Updation Unsuccessful";
        //    }
        //    return RedirectToAction("EmployeeSearch");
        //}
        //[HttpPut]
        //[Route("api/PutUserEmail")]
        //public IHttpActionResult PutEmail(string email )
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest("Not a valid model");
        //    using (var emp = new EmployeeDBEntities())
        //    {
        //        var existingUser = emp.UserLogins.Where(u => u.UserEmail == email.UserName).First<UserLogin>(); 

        //        if(existingUser != null)
        //        {
        //            existingUser.UserEmail = user.UserEmail;

        //            emp.SaveChanges();
        //        }
        //        else
        //        {
        //            return NotFound();
        //        }
        //    }

        //    return Ok();

        //}

        //[HttpGet]
        //[Route("api/GetUserClaims")]
        //public IHttpActionResult GetDet()
        //{
        //    ClaimsIdentity claimsIdentity = User.Identity as ClaimsIdentity;

        //    var claims = claimsIdentity.Claims.Select(x => new { Type = x.Type, Value = x.Value });

        //    return Ok(claims);
        //}
        //    [HttpGet]

        //    public Object GetToken()
        //    {
        //        string key = "my_secret_key_12345"; //Secret key which will be used later during validation    
        //        var issuer = "http://mysite.com";  //normally this will be your site URL    

        //        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        //        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //        //Create a List of Claims, Keep claims name short    
        //        var permClaims = new List<Claim>();
        //        permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        //        permClaims.Add(new Claim("valid", "1"));
        //        permClaims.Add(new Claim("userid", "1"));
        //        permClaims.Add(new Claim("name", "bilal"));

        //        //Create Security Token object by giving required parameters    
        //        var token = new JwtSecurityToken(issuer, //Issure    
        //                        issuer,  //Audience    
        //                        permClaims,
        //                        expires: DateTime.Now.AddDays(1),
        //                        signingCredentials: credentials);
        //        var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
        //        return new { data = jwt_token };
        //    }
        //    [HttpPost]
        //    public String GetName1()
        //    {
        //        if (User.Identity.IsAuthenticated)
        //        {
        //            var identity = User.Identity as ClaimsIdentity;
        //            if (identity != null)
        //            {
        //                IEnumerable<Claim> claims = identity.Claims;
        //            }
        //            return "Valid";
        //        }
        //        else
        //        {
        //            return "Invalid";
        //        }
        //    }

        //    [Authorize]
        //    [HttpPost]
        //    public Object GetName2()
        //    {
        //        var identity = User.Identity as ClaimsIdentity;
        //        if (identity != null)
        //        {
        //            IEnumerable<Claim> claims = identity.Claims;
        //            var name = claims.Where(p => p.Type == "name").FirstOrDefault()?.Value;
        //            return new
        //            {
        //                data = name
        //            };

        //        }
        //        return null;
        //    }
        //        [HttpGet]
        //        public Object GetToken()
        //        {
        //            string key = "my_secret_key_12345"; //Secret key which will be used later during validation    
        //            var issuer = "http://mysite.com";  //normally this will be your site URL    

        //            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        //            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //            //Create a List of Claims, Keep claims name short    
        //            var permClaims = new List<Claim>();
        //            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        //            permClaims.Add(new Claim("valid", "1"));
        //            permClaims.Add(new Claim("userid", "1"));
        //            permClaims.Add(new Claim("name", "bilal"));

        //            //Create Security Token object by giving required parameters    
        //            var token = new JwtSecurityToken(issuer, //Issure    
        //                            issuer,  //Audience    
        //                            permClaims,
        //                            expires: DateTime.Now.AddDays(1),
        //                            signingCredentials: credentials);
        //            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
        //            return new { data = jwt_token };
        //        }
    }
}
