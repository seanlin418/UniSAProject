using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using UniSA.Api.Repos;

namespace UniSA.Api.Controller
{
    public class BaseApiController : ApiController
    {
        //Helper method used to validate "userViewModel" and return corresponding HTTP status code.
        protected IHttpActionResult GetErrorResult(OpResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.GetAggregatedErrors() != null)
                {
                    foreach (string error in result.GetAggregatedErrors())
                    {
                        ModelState.AddModelError("innerExceptions", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }

    public class ModelFactory
    {
        
    }
}