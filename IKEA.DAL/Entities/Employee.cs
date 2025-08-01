﻿using Manage.DAL.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.DAL.Entities
{
    public class Employee : BaseEntity
    {
       
        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        public string ? Address { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
       
        public string? PhoneNumber { get; set; }
        public DateTime HiringDate { get; set; }

        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }

        public int? DepartmentId { get; set; }

        // Navigational Property [One]
        public virtual Department? Department { get; set; }
      
         public string? ImageUrl { get; set; }


    }
}
