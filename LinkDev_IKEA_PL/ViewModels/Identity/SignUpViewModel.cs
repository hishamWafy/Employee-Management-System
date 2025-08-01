using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace Manage.PL.ViewModels.Identity
{
    public class SignUpViewModel
    {
        [Display(Name = "First Name")]
       public string FirstName { get; set; } = null!;
        [Display(Name = "First Name")]
        public string LastName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; } = null!;
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = null!;
        public bool ISAgree { get; set; }
    }
}
