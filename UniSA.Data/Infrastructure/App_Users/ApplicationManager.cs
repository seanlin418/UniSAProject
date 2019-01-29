using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.AspNet.Identity.Owin;

namespace UniSA.Data
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext owinContext)
        {
            var context = owinContext.Get<ApplicationDbContext>();
            var store = new UserStore<ApplicationUser>(context);
            return new ApplicationUserManager(store);
        }
    }
}
