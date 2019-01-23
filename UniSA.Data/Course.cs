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
            DatesAndTimes = new HashSet<DateAndTimeInfo>();
        }
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Campus> Campuses { get; set; }
        public double CreditPoints { get; set; }
        public string EligibilityAndRequirementId { get; set; }
        public EligibilityAndRequirement EligibilityAndRequirement { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<DateAndTimeInfo> DatesAndTimes { get; set; }
    }
    public enum Availability { NotAvailable = 0, Semester1 = 1, Semester2 = 2, Summer = 3 }
    public enum Campus { NotAvailable = 0, Magill, MawsonLakes, AdelaideCE, AdelaideCW, MountGambier }
    public enum Offering { NotAvailable = 0,  OnCampas, External, Online }

    public class EligibilityAndRequirement
    {
        public EligibilityAndRequirement()
        {
            Prerequisites = new HashSet<Subject>();
            Corequisites = new HashSet<Subject>();
            NonAllowedSubjects = new HashSet<Subject>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string SubjectId { get; set; }
        public Subject Subject { get; set; }
        
        public virtual ICollection<Subject> Prerequisites { get; set; }
        public virtual ICollection<Subject> Corequisites { get; set; }
        public virtual ICollection<Subject> NonAllowedSubjects { get; set; }  
    }

    public class Assignment
    {
        public string Id { get; set; }
        public string Description { get; set; }

    }

    public class DateAndTimeInfo
    {
        public int Id { get; set; }
        public Availability Availability { get; set; }
        public Offering Offering { get; set; }
        public int TotalHourCommitment { get; set; }
        
        DateTime StartPeriod { get; set; }
        DateTime EndPeriod { get; set; }
        DateTime LastSelfEnrolDate { get; set; }
        DateTime CensusDate { get; set; }
        DateTime LastDateToWithdrawWithoutFail { get; set; }
       
        public string PrincipalCoordinatorId { get; set; }
        public Teacher PrincipalCoordinator { get; set; }
    }


    public class Course : DeletableEntity
    {
        public Course()
        {
            Subjects = new HashSet<Subject>();
        }
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public double TotalCreditPoints { get; set; }
        
        public virtual ICollection<Subject> Subjects { get; set; }
    }

    public class Teacher : DeletableEntity
    {
        public Teacher()
        {
            Subjects = new HashSet<Subject>();
        }
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public ICollection<Subject> Subjects { get; set; }
    }


    //public class Student : DeletableEntity
    //{
    //    public Student()
    //    {
    //        Subjects = new HashSet<Subject>();
    //        Courses = new HashSet<Course>();
    //    }
    //    public string Id { get; set; }
    //    public string ApplicationUserId { get; set; }
    //    public ApplicationUser ApplicationUser { get; set; }

    //    public ICollection<Subject> Subjects { get; set; }
    //    public ICollection<Course> Courses { get; set; }
    //}

}
