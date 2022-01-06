using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExceptionHandling.Controllers
{
    public class BuggyController : BaseAPIController
    {
         
        public BuggyController( )
        {
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            // return "secret text";
            return Unauthorized("Unauthorized");
        }
        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            string thingToReturn1= null;
            var thingToReturn = thingToReturn1;
            thingToReturn1.ToString();
            return thingToReturn;
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            string thingToReturn1 = null;
            var thingToReturn = thingToReturn1;
            thingToReturn1.ToString();
            return thingToReturn;
             
        }


    }
}
