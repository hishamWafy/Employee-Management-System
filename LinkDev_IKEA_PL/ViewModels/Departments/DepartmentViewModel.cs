using System.ComponentModel.DataAnnotations;

namespace Manage.PL.ViewModels.Departments
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The Code field is required.")]
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; } = null!;

        [Display(Name= "Creation Date")]
        public DateTime CreationDate { get; set; }



    }
}
