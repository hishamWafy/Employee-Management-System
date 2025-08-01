using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.DAL.Entities
{
    public class Department : BaseEntity
    {
           
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? Description { get; set; } = null!;

        public DateTime CreateDate { get; set; }

        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();


    }
}
