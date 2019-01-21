using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSA.Data;

namespace UniSA.Tester
{
    public class ApplicationManager : UserManager<ApplicationUser>
    {
        public ApplicationManager(IUserStore<ApplicationUser> store) : base(store)
        {
        }

        public static ApplicationManager Create(ApplicationDbContext context)
        {
            var store = new UserStore<ApplicationUser>(context);
            return new ApplicationManager(store);
        }
    }
}
