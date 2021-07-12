using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Angular_WebAPI.Models;
using Newtonsoft.Json;

namespace Angular_WebAPI.Controllers
{
    public class BlueprismController : ApiController
    {
        [HttpGet]
        [Route("api/localBP")]
        public IHttpActionResult getlocalprocess()
        {
            EmployeeDBEntities db = new EmployeeDBEntities();
            var results = db.localBPprocess().ToList();
            return Ok(results);
        }

        [HttpGet]
        [Route("api/processnames")]
        public IEnumerable<ProcessName_Result> getProcessNames()
        {
            EmployeeDBEntities db = new EmployeeDBEntities();
            var results = db.ProcessName();
            return results.ToList();

        }

        //[HttpGet]
        //[Route("api/processnames")]
        //public IHttpActionResult getProcessNames()
        //{
        //    EmployeeDBEntities db = new EmployeeDBEntities();
        //    var results = db.processnames().ToList();
        //    return Ok(results);

        //}
        [HttpGet]
        [Route("api/processdetails")]
        public IEnumerable<Proctest_Result> getProcessDetails(string proc )
        {
            EmployeeDBEntities db = new EmployeeDBEntities();
            var results = db.Proctest(proc);
            return results.ToList();
            
        }
        [HttpGet]
        [Route("api/XMLJson")]
        [AllowAnonymous]
        public object XMLJson()
        {



            string allText = System.IO.File.ReadAllText(@"C:\Users\lalit.sai.vedula\Downloads\Alerts.json");



            object jsonObject = JsonConvert.DeserializeObject(allText);
            return jsonObject;
        }

    }
}
