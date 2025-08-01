using System.ComponentModel.DataAnnotations;

namespace Manage.PL.ViewModels.Identity
{
    public class ForgetPasswordViewModel
    {


        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }



    }
}
