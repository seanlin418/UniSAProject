using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using UniSA.Data;

namespace UniSA.Tester
{
    class Program
    {
        static ApplicationManager appManager = null;

        static ApplicationDbContext context = null;

        static RoleManager<IdentityRole> roleManager = null;

        static void Main(string[] args)
        {
            context = new ApplicationDbContext();

            appManager = ApplicationManager.Create(context);

            roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            SeedRoles();

            //Create applicaiton user 
            ApplicationUser user = new ApplicationUser()
            {
                FirstName = "admin",
                LastName = "admin",
                UserName = "admin",
                Email = "xlinnn1@hotmail.com"
            };
            string password = "admin1";

            var result = SeedAdministrators(user, password);


        }

        public static void SeedRoles()
        {
            if (context.Roles.Any())
            {
                return;
            }


            if (!context.Roles.Any(w => w.Name == GlobalConstants.SuperAdministratorRoleName))
                roleManager.Create(new IdentityRole(GlobalConstants.SuperAdministratorRoleName));

            if (!context.Roles.Any(w => w.Name == GlobalConstants.AdministratorRoleName))
                roleManager.Create(new IdentityRole(GlobalConstants.AdministratorRoleName));


            if (!context.Roles.Any(w => w.Name == GlobalConstants.TeacherRoleName))
                roleManager.Create(new IdentityRole(GlobalConstants.TeacherRoleName));

            if (!context.Roles.Any(w => w.Name == GlobalConstants.StudentRoleName))
                roleManager.Create(new IdentityRole(GlobalConstants.StudentRoleName));

            context.SaveChanges();
        }

        public static IdentityResult SeedAdministrators(ApplicationUser user, string password)
        {
            IdentityResult result = appManager.Create(user, password);

            //Sign role of admin to the application user.
            if (result.Succeeded)
            {
                appManager.AddToRole(user.Id, GlobalConstants.AdministratorRoleName);

                Administrator admin = new Administrator();
                admin.ApplicationUserId = user.Id;
                context.Administrators.Add(admin);
                //Should catch exception for more details.
                context.SaveChanges();
            }

            return result;
        }

    }
}
