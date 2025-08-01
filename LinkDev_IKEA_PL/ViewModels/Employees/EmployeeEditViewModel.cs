using Manage.DAL.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Manage.PL.ViewModels.Employees
{
    public class EmployeeEditViewModel
    {

        //public int Id { get; set; }
        [Required(ErrorMessage = "The Name field is required.")]
        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        public string? Address { get;  set; } = null!;
        public string? Email { get; set; } = null!;
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; } = null!;
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
       
        [Display(Name = "Hiring Date")]
        public DateTime HiringDate { get; set; }
        public bool IsActive { get; set; }
        public Gender Gender { get; set; }
        [Display(Name= "Employee Type")]
        public EmployeeType EmployeeType { get; set; }
        [Display(Name = "Image")]
        public IFormFile? ImageUrl { get; set; }

    }
}
