using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniSA.Api.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace UniSA.Api.Repos
{
    public interface IUserRepository
    {
        Task<IdentityResult> RegisterUser(RegisterViewModel registerViewModel);
        Task<IdentityUser> FindUser(string userName, string password);
    }
}