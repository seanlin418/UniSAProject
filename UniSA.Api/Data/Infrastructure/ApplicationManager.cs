using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using UniSA.Api.Data.Common;

namespace UniSA.Api.Data
{
    public class ApplicationManager : UserManager<ApplicationUser>
    {
        public ApplicationManager(IUserStore<ApplicationUser> userStore)
            : base(userStore)
        {

        }

        public static ApplicationManager Create(IOwinContext context)
        {
            var appDbContext = context.Get<ApplicationDbContext>();
            return new ApplicationManager(new UserStore<ApplicationUser>(appDbContext));
        }
    }
}