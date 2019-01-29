using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniSA.Api.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using UniSA.Data;

namespace UniSA.Api.Repos
{
    public interface IUserRepository
    {
        Task<IdentityResult> Add(ApplicationUser newUser, string password);
        Task<IdentityResult> Delete(ApplicationUser user);
        Task<IdentityUser> FindUser(string userName, string password);
        Task<IdentityUser> FindById(string userId);
        Task<IdentityUser> FindByEmailAsync(string email);

    }
}