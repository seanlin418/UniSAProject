using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;

namespace UniSA.Api.Data.Common
{
    public class ApplicationUser : IdentityUser, IDeletableEntity
    {
        [Required(ErrorMessage = "Please enter a First Name")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter a Last Name")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string ImageUrl { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string DeletedBy { get; set; }
    }
}