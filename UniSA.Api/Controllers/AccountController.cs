using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using UniSA.Api.Repos;
using UniSA.Api.ViewModels;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Net.Http;
using UniSA.Api.Services;
using UniSA.Api.Data;

namespace UniSA.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private IAuthRepository _repo;
        public AccountController(IAuthRepository repo)
        {
            this._repo = repo;
        }

        [AllowAnonymous]
        [Route("Register")]
        public async Task<HttpResponseMessage> Register(HttpRequestMessage request, RegisterViewModel registerViewModel)
        {
            if (ModelState.Values.Count() > 0 )
            {
                return  request.CreateResponse(System.Net.HttpStatusCode.BadRequest,
                    ModelState.Values.SelectMany(w => w.Errors.Select(e => e.ErrorMessage)));
            }

            IdentityResult result = await _repo.RegisterUser(registerViewModel);

            HttpResponseMessage errorResultMessage = GetErrorResult(result, request);

            if (errorResultMessage != null)
            {
                return errorResultMessage;
            }

            return request.CreateResponse(System.Net.HttpStatusCode.OK);
        }

        //Helper method used to validate "userViewModel" and return corresponding HTTP status code.
        private HttpResponseMessage GetErrorResult(IdentityResult result, HttpRequestMessage request)
        {
            if (result == null)
            {
                return request.CreateResponse(System.Net.HttpStatusCode.InternalServerError);
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return request.CreateResponse(System.Net.HttpStatusCode.BadRequest);
                }

                return request.CreateResponse(System.Net.HttpStatusCode.BadRequest,
                    ModelState.Values.SelectMany(w => w.Errors.Select(e => e.ErrorMessage)));
            }

            return null;
        }

        //for debugging purpose..
        [AllowAnonymous]
        [Route("RegisterNewClient")]
        public async Task<IHttpActionResult> RegisterNewClient(Client client)
        {
            using (ClientRepository cr = new ClientRepository())
            {
                try
                {
                    await cr.AddClient(client);
                }
                catch (Exception ex)
                {
                    TextResult textResult = new TextResult(ex.Message, Request);
                    return textResult;
                }
            }

            return Ok();
        }
    }
}