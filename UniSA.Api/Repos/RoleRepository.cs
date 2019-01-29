using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UniSA.Data;

namespace UniSA.Api.Repos
{
    public class RoleRepository : IRoleRepository
    {
        private ApplicationDbContext _Ctx = null;
        private ApplicationRoleManager _RoleManager = null;
        private ApplicationUserManager _UserManager = null;
        public RoleRepository(ApplicationDbContext context)
        {
            this._Ctx = context;
            this._RoleManager = new ApplicationRoleManager(new RoleStore<IdentityRole>(context));
            this._UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
        }

        public async Task<IdentityResult> CreateRole(string roleName)
        {
            IdentityRole newRole = new IdentityRole(roleName);
            IdentityResult result = await this._RoleManager.CreateAsync(newRole);
            return result;
        }

        public async Task<OpResult> AddRoles(string userId, IEnumerable<string>roles, OpResult results = null)
        {
            if (results == null)
                results = new OpResult();

            ApplicationUser user = await this._UserManager.FindByIdAsync(userId);

            if (user == null)
            {
                results.Add(String.Format("User {0} does not exist", userId));
                return results;
            }

            foreach (var currRole in roles)
            {
                bool isRoleExist = await this._RoleManager.RoleExistsAsync(currRole);

                if (!isRoleExist)
                {
                    results.Add(String.Format("Role {0} does not exist.", currRole));
                }
                else
                {
                    bool isInRole = await this._UserManager.IsInRoleAsync(userId, currRole);
                    if (isInRole)
                    {
                        results.Add(String.Format("Add failed. User: {0} is already a {1}.", userId, currRole));
                    }
                    else
                    {
                        IdentityResult r = await this._UserManager.AddToRoleAsync(userId, currRole);
                        if (!r.Succeeded)
                            results.Add(r);
                    }
                }
            }

            return results;
        }

        public async Task<OpResult> RemoveRoles(string userId, IEnumerable<string> roles, OpResult results = null)
        {
            if (results == null)
                results = new OpResult();

            ApplicationUser user = await this._UserManager.FindByIdAsync(userId);

            if (user == null)
            {
                results.Add(String.Format("User {0} does not exist", userId));
                return results;
            }

            foreach (var currRole in roles)
            {
                bool isExist = await this._RoleManager.RoleExistsAsync(currRole);

                if (!isExist)
                {
                    results.Add(String.Format("Role {0} does not exist.", currRole));
                }
                else
                {
                    bool isInRole = await this._UserManager.IsInRoleAsync(userId, currRole);
                    if (isInRole)
                    {
                        IdentityResult r = await this._UserManager.RemoveFromRoleAsync(userId, currRole);
                        if (!r.Succeeded)
                            results.Add(r);
                    }
                    else
                    {
                        results.Add(String.Format("Remove failed. User: {0} is not a {1}.", userId, currRole));
                    }
                }
            }
            return results;
        }

        public async Task<IEnumerable<string>> GetRoles(string userId, OpResult results = null)
        {
            ApplicationUser user = await this._UserManager.FindByIdAsync(userId);

            if (user == null)
                return null;

            IList<string> items = await this._UserManager.GetRolesAsync(user.Id);
            return items;
        }
    }
}