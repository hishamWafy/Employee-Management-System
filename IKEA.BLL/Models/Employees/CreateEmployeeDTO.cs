using Manage.DAL.Common.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.BLL.Models.Employees
{
    public class  CreateEmployeeDTO
    {
       
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
        public string Name { get; set; } = null!;

        [Range(18, 65, ErrorMessage = "Age must be between 18 and 65.")]
        public int? Age { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9\s,.-]+$", ErrorMessage = "Address can only contain letters, numbers, spaces, commas, dots, and hyphens.")]
        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
        public string? Address { get; set; }


        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive value.")]
        public decimal Salary { get; set; }

        public bool IsActive { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "Invalid phone number.")]
        [StringLength(15, MinimumLength = 7, ErrorMessage = "Phone number must be between 7 and 15 digits.")]
        public string? PhoneNumber { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Hiring date is required.")]
        public DateTime HiringDate { get; set; }

        [EnumDataType(typeof(Gender), ErrorMessage = "Invalid gender type.")]
        public Gender Gender { get; set; }

        [EnumDataType(typeof(EmployeeType), ErrorMessage = "Invalid employee type.")]
        public EmployeeType EmployeeType { get; set; }

        public int DepartmentId { get; set; }

       // public IFormFile? Image { get; set; }
        public IFormFile? ImageUrl { get; set; }


    }
     


}
