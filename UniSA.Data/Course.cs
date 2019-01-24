using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSA.Data.Common;

namespace UniSA.Data
{
    public class Subject : DeletableEntity
    {
        public Subject()
        {
            Courses = new HashSet<Course>();
            Campuses = new HashSet<Campus>();
            DatesAndTimes = new HashSet<SubjectDatesAndTimesInfo>();

            Teachers = new HashSet<Teacher>();

            Prerequisites = new HashSet<Subject>();
            Corequisites = new HashSet<Subject>();
            NonAllowedSubjects = new HashSet<Subject>();
        }
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Campus> Campuses { get; set; }
        public double CreditPoints { get; set; }

        //Eligibility And Requirement
        public virtual ICollection<Subject> Prerequisites { get; set; }
        public virtual ICollection<Subject> Corequisites { get; set; }
        public virtual ICollection<Subject> NonAllowedSubjects { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<SubjectDatesAndTimesInfo> DatesAndTimes { get; set; }

        public virtual ICollection<Teacher> Teachers { get; set; }
    }
    public enum Availability { NotAvailable = 0, Semester1 = 1, Semester2 = 2, Summer = 3 }
    public enum Campus { NotAvailable = 0, Magill, MawsonLakes, AdelaideCE, AdelaideCW, MountGambier }
    public enum Offering { NotAvailable = 0,  OnCampas, External, Online }

    //public class Assignment
    //{
    //    public string Id { get; set; }
    //    public string Description { get; set; }

    //}

    public class SubjectDatesAndTimesInfo : DeletableEntity
    {
        public SubjectDatesAndTimesInfo()
        {
            PrincipalCoordinators = new HashSet<Teacher>();
        } 
        public int Id { get; set; }
        public Availability Availability { get; set; }
        public Offering Offering { get; set; }
        public int TotalHourCommitment { get; set; }

        DateTime StartPeriod { get; set; }
        DateTime EndPeriod { get; set; }
        DateTime LastSelfEnrolDate { get; set; }
        DateTime CensusDate { get; set; }
        DateTime LastDateToWithdrawWithoutFail { get; set; }

        public virtual ICollection<Teacher> PrincipalCoordinators { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
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

        public virtual ICollection<Subject> Subjects { get; set; }

        public virtual ICollection<Teacher> PrincipalCoordinators { get; set; }

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

        public IEnumerable<Subject> PrincipalCoordinatorOfSubjects
        {
            get
            {
                var subjects = Subjects.SelectMany(w => w.DatesAndTimes).Where(w => this.Subjects.Any(s => s.Id == w.SubjectId)).Select(w => w.Subject);
                return subjects;
            }
        }

        public IEnumerable<Course> PrincipalCoordinatorOfCourses
        {
            get
            {
                var courses = Courses.Where(w => w.PrincipalCoordinators.Any(s => s.ApplicationUserId == this.ApplicationUserId));
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
    }

}
