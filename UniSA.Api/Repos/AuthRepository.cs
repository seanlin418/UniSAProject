using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using UniSA.Api.ViewModels;
using UniSA.Api.Data;

namespace UniSA.Api.Repos
{
    public class AuthRepository : IAuthRepository, IDisposable
    {
        private ApplicationDbContext _ctx;
        private UserManager<IdentityUser> _userManager;

        //Empty constructor is required by Startup Configuration, 
        //which required signture of Configuration (IAppBuilder app) with only IAppBuilder as input param.
        public AuthRepository()
        {
            _ctx = new ApplicationDbContext();
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
        }

        public AuthRepository(ApplicationDbContext ctx)
        {
            this._ctx = ctx;
            this._userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
        }

        public async Task<IdentityResult> RegisterUser(RegisterViewModel registerViewModel)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = registerViewModel.UserName
            };

            var result = await _userManager.CreateAsync(user, registerViewModel.Password);

            return result;
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public void Dispose()
        {
            this._ctx.Dispose();
            this._userManager.Dispose();
        }

        
    }
}