namespace UniSA.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using UniSA.Data.AppClients;

    internal sealed class Configuration : DbMigrationsConfiguration<UniSA.Data.ApplicationDbContext>
    {
        private UserManager<ApplicationUser> userManager;
        private ApplicationUserManager CreateUserManager(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            //Configure application manager here.

            return userManager;
        }
        private RoleManager<IdentityRole> roleManager;


        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(UniSA.Data.ApplicationDbContext context)
        {
            if (System.Diagnostics.Debugger.IsAttached == false)
            {
                System.Diagnostics.Debugger.Launch();
            }

            this.userManager = CreateUserManager(context);
            this.roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            this.SeedClients(context);
            this.SeedRoles(context);
            this.SeedAdministrators(context);
        }

        private void SeedClients(ApplicationDbContext context)
        {
            if (context.Clients.Any())
                return;

            Client appClient = new Client()
            {
                Id = "UniSA.WebApp",
                Secret = Hasher.GetHash("UniSA.WebApp"),
                Name = "UniSA Web Application",
                ApplicationType = ApplicationType.JavaScript,
                Active = true,
                RefreshTokenLifeTime = 7200,
                AllowedOrigin = "http://localhost:49910"
            };

            context.Clients.Add(appClient);
            context.SaveChanges();
        }

        private void SeedRoles(ApplicationDbContext context)
        {
            if (context.Roles.Any()) { return; }

            context.Roles.AddOrUpdate(new IdentityRole(GlobalConstants.SuperAdministratorRoleName));
            context.Roles.AddOrUpdate(new IdentityRole(GlobalConstants.AdministratorRoleName));
            context.Roles.AddOrUpdate(new IdentityRole(GlobalConstants.TeacherRoleName));
            context.Roles.AddOrUpdate(new IdentityRole(GlobalConstants.StudentRoleName));

            context.SaveChanges();
        }

        private void SeedAdministrators(ApplicationDbContext context)
        {
            if (context.Administrators.Any()) { return; }

            //Add Super Administration
            Administrator superAdmin = new Administrator();
            ApplicationUser superAdminUser = new ApplicationUser()
            {
                FirstName = "superadmin",
                LastName = "superadmin",
                Email = "superadmin@superadmin.com",
                UserName = "superadmin"
            };
            var password = "superadmin";

            bool isAdded = this.SeedAdminApplicationUser(superAdminUser, password);

            if (isAdded)
                superAdmin.ApplicationUserId = superAdminUser.Id;

            context.Administrators.Add(superAdmin);

            context.SaveChanges();


            //Add Adminstration 
            Administrator admin = new Administrator();
            ApplicationUser adminUser = new ApplicationUser()
            {
                FirstName = "admin",
                LastName = "admin",
                Email = "admin@admin.com",
                UserName = "admin"
            };
            password = "adminadmin";

            isAdded = this.SeedAdminApplicationUser(adminUser, password);

            if (isAdded)
                admin.ApplicationUserId = adminUser.Id;

            context.Administrators.Add(admin);

            context.SaveChanges();
        }

        private bool SeedAdminApplicationUser(ApplicationUser adminUser, string password)
        {
            if (!this.roleManager.RoleExists(GlobalConstants.SuperAdministratorRoleName))
            {
                this.roleManager.Create(new IdentityRole(GlobalConstants.SuperAdministratorRoleName));
            }

            if (!this.roleManager.RoleExists(GlobalConstants.AdministratorRoleName))
            {
                this.roleManager.Create(new IdentityRole(GlobalConstants.AdministratorRoleName));
            }

            var result = this.userManager.Create(adminUser, password);

            if (result.Succeeded)
            {
                this.userManager.AddToRole(adminUser.Id, GlobalConstants.AdministratorRoleName);

                if (adminUser.UserName == "superadmin")
                {
                    this.userManager.AddToRole(adminUser.Id, GlobalConstants.SuperAdministratorRoleName);
                }

                return true;
            }
            else return false;
        }
    }
}
