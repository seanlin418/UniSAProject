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
using UniSA.Data;
using UniSA.Data.AppClients;

namespace UniSA.Api.Controller
{
    [RoutePrefix("api/Account")]
    public class AccountController : BaseApiController
    {
        private IUserRepository _repo;
        public AccountController(IUserRepository repo)
        {
            this._repo = repo;
        }

        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.Values.Count() > 0 )
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await _repo.RegisterUser(registerViewModel);

            var errorResultMessage = this.GetErrorResult(result);

            if (errorResultMessage != null)
            {
                return errorResultMessage;
            }

            return Ok();
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