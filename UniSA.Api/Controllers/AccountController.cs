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
using AutoMapper;

namespace UniSA.Api.Controller
{
    [RoutePrefix("api/Account")]
    public class AccountController : BaseApiController
    {
        private IAuthService  _AuthService = null;
        public AccountController(IAuthService authService)
        {
            this._AuthService = authService;
        }

        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterViewModel registerVm)
        {
            if (ModelState.Values.Count() > 0 )
            {
                return BadRequest(ModelState);
            }

            ApplicationUser newUser = Mapper.Map<RegisterViewModel, ApplicationUser>(registerVm);

            OpResult result = await _AuthService.RegisterUser(newUser, registerVm.Password, registerVm.Roles);

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