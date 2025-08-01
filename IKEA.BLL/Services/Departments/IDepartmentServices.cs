using Manage.BLL.Models.Dpartments;
using Manage.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.BLL.Services.Departments
{
    public interface IDepartmentServices
    {
       Task<IEnumerable<DepartmentDTO>> GetAllDepartmentsAsync();
        Task<DepartmentDetailsDTO?> GetDepartmentByIdAsync(int id);

        Task<int> CreateDepartmentAsync(CreatedDepartmentDTO department);
        Task<int> UpdateDepartmentAsync(UpdateDepartmentDTO department);
        Task<bool> DeleteDepartmentAsync(int id);
    }
}
