using Manage.DAL.Common.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.BLL.Models.Employees
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
       
        public string? Email { get; set; }
        public string Gender { get; set; } = null!;
        public string EmployeeType { get; set; } = null!;

        public string Department { get; set; } = null!;

        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }




    }
}
