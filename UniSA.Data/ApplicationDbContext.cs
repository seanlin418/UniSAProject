namespace UniSA.Data
{
    using System.Data.Entity;
    using System.Linq;
    using System.Data.Entity.Validation;
    using Microsoft.AspNet.Identity.EntityFramework;
    using UniSA.Data.AppClients;
    using UniSA.Data;

    public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
            
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public new void SaveChanges()
        {
            try
            {
                base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }


        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<Administrator> Administrators { get; set; }

        public virtual DbSet<Subject> Subjects { get; set; }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<SubjectDatesAndTimesInfo> SubjectDatesAndTimesInfos { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Subject>()
                .HasMany(t => t.Prerequisites)
                .WithMany()
                .Map(m =>
                {
                    m.ToTable("SubjectPrerequisite");
                    m.MapLeftKey("Id");
                    m.MapRightKey("PrerequisitesId");
                });
            modelBuilder.Entity<Subject>()
                .HasMany(t => t.Corequisites)
                .WithMany()
                .Map(m =>
                {
                    m.ToTable("SubjectCorequisite");
                    m.MapLeftKey("Id");
                    m.MapRightKey("CorequisitesId");
                });
            modelBuilder.Entity<Subject>()
                .HasMany(t => t.NonAllowedSubjects)
                .WithMany()
                .Map(m =>
                {
                    m.ToTable("SubjectNonAllowedSubjects");
                    m.MapLeftKey("Id");
                    m.MapRightKey("NonAllowedSubjectsId");
                });

            modelBuilder.Entity<Course>()
                .HasMany(t => t.Subjects)
                .WithMany(t => t.Courses)
                .Map(m =>
                {
                    m.ToTable("CourseSubject");
                    m.MapLeftKey("CourseId");
                    m.MapRightKey("SubjectId");
                });
        }
    }
}
