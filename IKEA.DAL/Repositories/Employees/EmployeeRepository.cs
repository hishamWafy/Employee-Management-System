using Manage.DAL.Data.Contexts;
using Manage.DAL.Entities;
using Manage.DAL.Repositories._Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.DAL.Repositories.Employees
{
    public class EmployeeRepository : GenericRepository<Employee> ,IEmployeeRepository
    {

        public EmployeeRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            
        }

    }
}
