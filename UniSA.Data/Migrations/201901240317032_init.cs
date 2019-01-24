namespace UniSA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Administrators",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        ApplicationUserId = c.String(maxLength: 128),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        DeletedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        ImageUrl = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        DeletedBy = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Secret = c.String(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        ApplicationType = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        RefreshTokenLifeTime = c.Int(nullable: false),
                        AllowedOrigin = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RefreshTokens",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Subject = c.String(nullable: false, maxLength: 50),
                        ClientId = c.String(nullable: false, maxLength: 50),
                        IssuedUtc = c.DateTime(nullable: false),
                        ExpiresUtc = c.DateTime(nullable: false),
                        ProtectedTicket = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        CreditPoints = c.Double(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        DeletedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        TotalCreditPoints = c.Double(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        DeletedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubjectDatesAndTimesInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Availability = c.Int(nullable: false),
                        Offering = c.Int(nullable: false),
                        TotalHourCommitment = c.Int(nullable: false),
                        PrincipalCoordinatorId = c.String(),
                        SubjectId = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        DeletedBy = c.String(),
                        Subject_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subjects", t => t.Subject_Id)
                .Index(t => t.Subject_Id);
            
            CreateTable(
                "dbo.SubjectCorequisite",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        CorequisitesId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.CorequisitesId })
                .ForeignKey("dbo.Subjects", t => t.Id)
                .ForeignKey("dbo.Subjects", t => t.CorequisitesId)
                .Index(t => t.Id)
                .Index(t => t.CorequisitesId);
            
            CreateTable(
                "dbo.CourseSubject",
                c => new
                    {
                        CourseId = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CourseId, t.SubjectId })
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.SubjectNonAllowedSubjects",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        NonAllowedSubjectsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.NonAllowedSubjectsId })
                .ForeignKey("dbo.Subjects", t => t.Id)
                .ForeignKey("dbo.Subjects", t => t.NonAllowedSubjectsId)
                .Index(t => t.Id)
                .Index(t => t.NonAllowedSubjectsId);
            
            CreateTable(
                "dbo.SubjectPrerequisite",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        PrerequisitesId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.PrerequisitesId })
                .ForeignKey("dbo.Subjects", t => t.Id)
                .ForeignKey("dbo.Subjects", t => t.PrerequisitesId)
                .Index(t => t.Id)
                .Index(t => t.PrerequisitesId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubjectPrerequisite", "PrerequisitesId", "dbo.Subjects");
            DropForeignKey("dbo.SubjectPrerequisite", "Id", "dbo.Subjects");
            DropForeignKey("dbo.SubjectNonAllowedSubjects", "NonAllowedSubjectsId", "dbo.Subjects");
            DropForeignKey("dbo.SubjectNonAllowedSubjects", "Id", "dbo.Subjects");
            DropForeignKey("dbo.SubjectDatesAndTimesInfoes", "Subject_Id", "dbo.Subjects");
            DropForeignKey("dbo.CourseSubject", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.CourseSubject", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.SubjectCorequisite", "CorequisitesId", "dbo.Subjects");
            DropForeignKey("dbo.SubjectCorequisite", "Id", "dbo.Subjects");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Administrators", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.SubjectPrerequisite", new[] { "PrerequisitesId" });
            DropIndex("dbo.SubjectPrerequisite", new[] { "Id" });
            DropIndex("dbo.SubjectNonAllowedSubjects", new[] { "NonAllowedSubjectsId" });
            DropIndex("dbo.SubjectNonAllowedSubjects", new[] { "Id" });
            DropIndex("dbo.CourseSubject", new[] { "SubjectId" });
            DropIndex("dbo.CourseSubject", new[] { "CourseId" });
            DropIndex("dbo.SubjectCorequisite", new[] { "CorequisitesId" });
            DropIndex("dbo.SubjectCorequisite", new[] { "Id" });
            DropIndex("dbo.SubjectDatesAndTimesInfoes", new[] { "Subject_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Administrators", new[] { "ApplicationUserId" });
            DropTable("dbo.SubjectPrerequisite");
            DropTable("dbo.SubjectNonAllowedSubjects");
            DropTable("dbo.CourseSubject");
            DropTable("dbo.SubjectCorequisite");
            DropTable("dbo.SubjectDatesAndTimesInfoes");
            DropTable("dbo.Courses");
            DropTable("dbo.Subjects");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.RefreshTokens");
            DropTable("dbo.Clients");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Administrators");
        }
    }
}
