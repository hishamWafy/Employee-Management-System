using Manage.DAL.Repositories.Departments;
using Manage.DAL.Repositories.Employees; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace Manage.DAL.Unit_Of_Work
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IEmployeeRepository EmployeeRepository { get; }
        public  IDepartmentReopsitory DepartmentRepository { get; }

        Task<int> CompleteAsync();









    }
}
