using Manage.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.DAL.Repositories._Generic
{
    public interface IGenericRepository<T> where T : BaseEntity
    {

        public Task<IEnumerable<T>> GetAllAsync (bool withAsTracking = true);
        public IQueryable<T> GetAllAsIQueryable();

        IEnumerable<T> GetIEnumerable();

        public Task<T?> GetByIdAsync(int id);
        public void Add(T entity);
        public void Update(T entity);
        public void Delete(T entity);



    }
}
