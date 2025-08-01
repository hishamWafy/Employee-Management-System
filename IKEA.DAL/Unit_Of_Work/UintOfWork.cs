using Manage.DAL.Data.Contexts;
using Manage.DAL.Repositories.Departments;
using Manage.DAL.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.DAL.Unit_Of_Work
{
    public class UintOfWork : IUnitOfWork 
    {
        private readonly ApplicationDbContext _dbContext;

        public IEmployeeRepository EmployeeRepository => new EmployeeRepository (_dbContext);
        public IDepartmentReopsitory DepartmentRepository => new DepartmentRepository(_dbContext);
        public UintOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CompleteAsync()
        {
           return await _dbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {  
          await _dbContext.DisposeAsync();
        }
    }
}
