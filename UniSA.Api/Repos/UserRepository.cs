using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using UniSA.Api.ViewModels;
using UniSA.Data;

namespace UniSA.Api.Repos
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private ApplicationDbContext _ctx;
        private UserManager<ApplicationUser> _userManager;

        //Empty constructor is required by Startup Configuration, 
        //which required signture of Configuration (IAppBuilder app) with only IAppBuilder as input param.
        public UserRepository()
        {
            _ctx = new ApplicationDbContext();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_ctx));
        }

        public UserRepository(ApplicationDbContext ctx)
        {
            this._ctx = ctx;
            this._userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_ctx));
        }

        public async Task<IdentityResult> Add(ApplicationUser newUser, string password)
        {
            IdentityResult result = null;

            var existUser = await this._userManager.FindByEmailAsync(newUser.Email);

            if(existUser != null)
            {
                result = new IdentityResult(new String[] { String.Format("{0} is already a Microsoft account. Try another name. If it's yours, sign in now.", newUser.Email) });
                return result;
            }

            result = await _userManager.CreateAsync(newUser, password);
            return result;
        }

        public async Task<IdentityResult> Delete(ApplicationUser newUser)
        {
            var result = await _userManager.DeleteAsync(newUser);
            return result;
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public async Task<IdentityUser> FindById(string userId)
        {
            IdentityUser user = await _userManager.FindByIdAsync(userId);

            return user;
        }

        public async Task<IdentityUser> FindByEmailAsync(string email)
        {
            IdentityUser user = await _userManager.FindByEmailAsync(email);
            return user;
        }


        public void Dispose()
        {
            this._ctx.Dispose();
            this._userManager.Dispose();
        }

        
    }
}