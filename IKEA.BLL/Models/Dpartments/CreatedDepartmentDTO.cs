 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.BLL.Models.Dpartments
{
    public class CreatedDepartmentDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Code is required")]

        public string Code { get; set; } = null!;
        public string? Description { get; set; } = null!;

        public DateTime CreateDate { get; set; }  





    }
}
