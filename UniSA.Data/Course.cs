using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSA.Data.Common;

namespace UniSA.Data
{
    public enum Availability { NotAvailable = 0, Semester1 = 1, Semester2 = 2, Summer = 3, Winter = 4}
    public enum Campus { NotAvailable = 0, Magill, MawsonLakes, AdelaideCE, AdelaideCW, MountGambier }
    public enum Offering { NotAvailable = 0,  OnCampas, External, Online }

    public class AcademicYear : DeletableEntity
    {
        public int Id { get; set; }
        DateTime StartPeriod { get; set; }
        DateTime EndPeriod { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
    }

    public class Subject : DeletableEntity
    {
        public Subject()
        {
            Courses = new HashSet<Course>();
            Campuses = new HashSet<Campus>();

            Prerequisites = new HashSet<Subject>();
            Corequisites = new HashSet<Subject>();
            NonAllowedSubjects = new HashSet<Subject>();

            Teachers = new HashSet<Teacher>();
            PrincipalCoordinators = new HashSet<Teacher>();
        }
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Campus> Campuses { get; set; }
        public double CreditPoints { get; set; }
        public Availability Availability { get; set; }
        public Offering Offering { get; set; }
        public int TotalHourCommitment { get; set; }
        DateTime StartPeriod { get; set; }
        DateTime EndPeriod { get; set; }
        DateTime LastSelfEnrolDate { get; set; }
        DateTime CensusDate { get; set; }
        DateTime LastDateToWithdrawWithoutFail { get; set; }

        //Eligibility And Requirement
        public virtual ICollection<Subject> Prerequisites { get; set; }
        public virtual ICollection<Subject> Corequisites { get; set; }
        public virtual ICollection<Subject> NonAllowedSubjects { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<Teacher> PrincipalCoordinators { get; set; }
        public virtual ICollection<AcademicYear> AcademicYears { get; set; }
    }

    public class Course : DeletableEntity
    {
        public Course()
        {
            Subjects = new HashSet<Subject>();
            PrincipalCoordinators = new HashSet<Teacher>();
        }
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public double TotalCreditPoints { get; set; }

        public int AcademicYearId { get; set; }
        public AcademicYear AcademicYear { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }
        public virtual ICollection<Teacher> PrincipalCoordinators { get; set; }
        public virtual ICollection<AcademicYear> AcademicYears { get; set; }

    }

    public class Teacher : DeletableEntity
    {
        public Teacher()
        {
            Subjects = new HashSet<Subject>();
        }
        public int Id { get; set; }
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<AcademicYear> AcademicYears { get; set; }

        public IEnumerable<Subject> PrincipalCoordinatorOfSubjects
        {
            get
            {
                var subjects = Subjects.Where(w => w.PrincipalCoordinators.Any(s => s.ApplicationUserId == this.ApplicationUserId)).Select(w => w);
                return subjects;
            }
        }

        public IEnumerable<Course> PrincipalCoordinatorOfCourses
        {
            get
            {
                var courses = Courses.Where(w => w.PrincipalCoordinators.Any(s => s.ApplicationUserId == this.ApplicationUserId)).Select(w => w);
                return courses;
            }
        }

    }


    public class Student : DeletableEntity
    {
        public Student()
        {
            Subjects = new HashSet<Subject>();
            Courses = new HashSet<Course>();
        }
        public string Id { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<AcademicYear> AcademicYears { get; set; }
    }

}
