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
        private ApplicationDbContext ctx = null;
        private ApplicationRoleManager roleManager = null;
        private ApplicationUserManager userManager = null;
        public RoleRepository(ApplicationDbContext context)
        {
            this.ctx = context;
            this.roleManager = new ApplicationRoleManager(new RoleStore<IdentityRole>(context));
            this.userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
        }

        public async Task<IdentityResult> CreateRole(string roleName)
        {
            IdentityRole newRole = new IdentityRole(roleName);
            var result = await this.roleManager.CreateAsync(newRole);
            return result;
        }

        public async Task<List<string>> AddRoles (string userId, List<string>roles)
        {
            List<string> errors = new List<string>();
            foreach(var currRole in roles)
            {
                IdentityResult result = await this.userManager.AddToRoleAsync(userId, currRole);
                
                if (!result.Succeeded)
                    errors.AddRange(result.Errors);
            }
            return errors;
        }
    }
}