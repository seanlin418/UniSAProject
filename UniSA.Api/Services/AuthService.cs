using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UniSA.Api.Repos;
using UniSA.Api.ViewModels;
using UniSA.Data;

namespace UniSA.Api.Services
{
    public class AuthService : IAuthService
    {
        private IUserRepository _UserRepo = null;
        private IRoleRepository _RoleRepo = null;
        public AuthService(IUserRepository userRepo, IRoleRepository roleRepo)
        {
            this._UserRepo = userRepo;
            this._RoleRepo = roleRepo;
        }

        public async Task<OpResult> RegisterUser(ApplicationUser newUser, string password, List<string> roles = null)
        {
            var r = await this._UserRepo.Add(newUser, password);

            OpResult results = new OpResult(r.Errors.ToArray());

            if (!results.Succeeded)
            {
                return results; 
            }

            if (roles == null)
                return results;

            return await this._RoleRepo.AddRoles(newUser.Id, roles, results);
        }

        public async Task<OpResult> DeleteUser(ApplicationUser user)
        {
            var roles = await this._RoleRepo.GetRoles(user.Id);

            var results = new OpResult();
            if(roles != null)
            {
                results = await this._RoleRepo.RemoveRoles(user.Id, roles);

                if (!results.Succeeded)
                {
                    results.Add(String.Format("Fail to remove roles from user: {0}", user.Id));
                    return results;
                }
            }

            var r = await this._UserRepo.Delete(user);
            results.Add(r);
            return results;
        }

        public async Task<OpResult> AddUserRoles(ApplicationUser user, List<string> roles)
        {
            OpResult results = await this._RoleRepo.AddRoles(user.Id, roles);
            return results;
        }

        public async Task<OpResult> RemoveUserRoles(ApplicationUser user, List<string> roles)
        {
            OpResult results = await this._RoleRepo.RemoveRoles(user.Id, roles);
            return results;
        }
    }
}