using Manage.BLL.Models.Dpartments;
using Manage.BLL.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.BLL.Services.Employees
{
    public interface IEmployeeServices
    {

        Task<IEnumerable<EmployeeDTO>> GetAllEmployeesAsync(string search);
       Task<DetailsEmployeeDTO?> GetEmployeeByIdAsync(int id);

        Task<int> CreateEmployeeAsync(CreateEmployeeDTO employee);
        Task<int> UpdateEmployeeAsync(UpdateEmplooyeeDTO employee);
        Task<bool> DeleteEmployeeAsync(int id);






    }
}
