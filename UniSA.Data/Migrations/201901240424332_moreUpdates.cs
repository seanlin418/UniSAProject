namespace UniSA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class moreUpdates : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SubjectDatesAndTimesInfoes", "Subject_Id", "dbo.Subjects");
            DropIndex("dbo.SubjectDatesAndTimesInfoes", new[] { "Subject_Id" });
            DropColumn("dbo.SubjectDatesAndTimesInfoes", "SubjectId");
            RenameColumn(table: "dbo.SubjectDatesAndTimesInfoes", name: "Subject_Id", newName: "SubjectId");
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        DeletedBy = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        SubjectDatesAndTimesInfo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.SubjectDatesAndTimesInfoes", t => t.SubjectDatesAndTimesInfo_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.SubjectDatesAndTimesInfo_Id);
            
            CreateTable(
                "dbo.TeacherCourses",
                c => new
                    {
                        Teacher_Id = c.Int(nullable: false),
                        Course_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Teacher_Id, t.Course_Id })
                .ForeignKey("dbo.Teachers", t => t.Teacher_Id, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.Course_Id, cascadeDelete: true)
                .Index(t => t.Teacher_Id)
                .Index(t => t.Course_Id);
            
            CreateTable(
                "dbo.SubjectTeachers",
                c => new
                    {
                        Subject_Id = c.Int(nullable: false),
                        Teacher_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Subject_Id, t.Teacher_Id })
                .ForeignKey("dbo.Subjects", t => t.Subject_Id, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.Teacher_Id, cascadeDelete: true)
                .Index(t => t.Subject_Id)
                .Index(t => t.Teacher_Id);
            
            AlterColumn("dbo.SubjectDatesAndTimesInfoes", "SubjectId", c => c.Int(nullable: false));
            AlterColumn("dbo.SubjectDatesAndTimesInfoes", "SubjectId", c => c.Int(nullable: false));
            CreateIndex("dbo.SubjectDatesAndTimesInfoes", "SubjectId");
            AddForeignKey("dbo.SubjectDatesAndTimesInfoes", "SubjectId", "dbo.Subjects", "Id", cascadeDelete: true);
            DropColumn("dbo.SubjectDatesAndTimesInfoes", "PrincipalCoordinatorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SubjectDatesAndTimesInfoes", "PrincipalCoordinatorId", c => c.String());
            DropForeignKey("dbo.SubjectDatesAndTimesInfoes", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.SubjectTeachers", "Teacher_Id", "dbo.Teachers");
            DropForeignKey("dbo.SubjectTeachers", "Subject_Id", "dbo.Subjects");
            DropForeignKey("dbo.Teachers", "SubjectDatesAndTimesInfo_Id", "dbo.SubjectDatesAndTimesInfoes");
            DropForeignKey("dbo.TeacherCourses", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.TeacherCourses", "Teacher_Id", "dbo.Teachers");
            DropForeignKey("dbo.Teachers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.SubjectTeachers", new[] { "Teacher_Id" });
            DropIndex("dbo.SubjectTeachers", new[] { "Subject_Id" });
            DropIndex("dbo.TeacherCourses", new[] { "Course_Id" });
            DropIndex("dbo.TeacherCourses", new[] { "Teacher_Id" });
            DropIndex("dbo.SubjectDatesAndTimesInfoes", new[] { "SubjectId" });
            DropIndex("dbo.Teachers", new[] { "SubjectDatesAndTimesInfo_Id" });
            DropIndex("dbo.Teachers", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.SubjectDatesAndTimesInfoes", "SubjectId", c => c.Int());
            AlterColumn("dbo.SubjectDatesAndTimesInfoes", "SubjectId", c => c.String());
            DropTable("dbo.SubjectTeachers");
            DropTable("dbo.TeacherCourses");
            DropTable("dbo.Teachers");
            RenameColumn(table: "dbo.SubjectDatesAndTimesInfoes", name: "SubjectId", newName: "Subject_Id");
            AddColumn("dbo.SubjectDatesAndTimesInfoes", "SubjectId", c => c.String());
            CreateIndex("dbo.SubjectDatesAndTimesInfoes", "Subject_Id");
            AddForeignKey("dbo.SubjectDatesAndTimesInfoes", "Subject_Id", "dbo.Subjects", "Id");
        }
    }
}
