using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using UniSA.Api.Controller;

namespace UniSA.Controllers
{
    [RoutePrefix("api/Orders")]
    public class OrdersController : BaseApiController
    {
        [Authorize]
        [Route("")]
        public IHttpActionResult Get()
        {
            Random r = new Random();
            return Ok("You are authorised. " + r.Next(0, 100).ToString());
        }

    }
}