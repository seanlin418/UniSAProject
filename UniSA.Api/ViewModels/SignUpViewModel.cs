using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniSA.Api.ViewModels

{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "Please enter a Password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,10}$", ErrorMessage = "Minimum eight and maximum 10 characters, at least one uppercase letter, one lowercase letter, one number and one special character")]
        [Display(Name = "Password")]
        public string Password { get; set; }


        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Please enter an email address")]
        [RegularExpression(@"^[a-zA-Z0-9'._%+-]+@[a-zA-Z0-9-][a-zA-Z0-9.-]*\.[a-zA-Z]{2,9}$", ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
    }
}