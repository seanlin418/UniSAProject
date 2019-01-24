namespace UniSA.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<UniSA.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(UniSA.Data.ApplicationDbContext context)
        {
            //if (System.Diagnostics.Debugger.IsAttached == false)
            //{
            //    System.Diagnostics.Debugger.Launch();
            //}

            //if (!context.Subjects.Any())
            //{
            //    Subject audit = new Subject()
            //    {
            //        Code = "ACCT90003",
            //        Name = "Audit",
            //        CreditPoints = 12.5
            //    };
            //    audit.Campuses.Add(Campus.Magill);

            //    audit.Prerequisites.Add(new Subject()
            //    {
            //        Code = "MAT10001",
            //        Name = "Math",
            //        CreditPoints = 12.5
            //    });
            //    audit.Corequisites.Add(new Subject()
            //    {
            //        Code = "ENG10001",
            //        Name = "English",
            //        CreditPoints = 12.5
            //    });

            //    audit.NonAllowedSubjects.Add(new Subject()
            //    {
            //        Code = "GAME10001",
            //        Name = "Game",
            //        CreditPoints = 12.5
            //    });

            //    context.Subjects.Add(audit);
            //    context.SaveChanges();
            //}
            //else
            //{

            //}
        }
    }
}
