using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.BLL.Models.Dpartments
{
    public class UpdateDepartmentDTO
    {

        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? Description { get; set; } = null!;

        public DateTime CreateDate { get; set; }


        //public bool IsDeleted { get; set; }
        //public int CreatedBy { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public int LastModifiedBy { get; set; }
        //public DateTime LastModifiyAt { get; set; }






    }
}
