using Manage.DAL.Data.Contexts;
using Manage.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.DAL.Repositories._Generic
{
    public class GenericRepository<T> :IGenericRepository<T> where T : BaseEntity 
    {

        private readonly ApplicationDbContext _dbContext;
        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> GetAllAsIQueryable()
        {
            return _dbContext.Set<T>();
        }

        public IEnumerable<T> GetIEnumerable()
        {
            return _dbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync(bool withAsTracking = true)
        {
            if (withAsTracking)
            {
                return await _dbContext.Set<T>().Where(x => !x.IsDeleted).AsNoTracking().ToListAsync();
            }
            return await _dbContext.Set<T>().Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            var employee = _dbContext.Set<T>().FindAsync(id);
            return await employee;
        }

        public void Add(T entity) =>  _dbContext.Set<T>().Add(entity);

        public void Update(T entity) => _dbContext.Set<T>().Update(entity);


        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            _dbContext.Set<T>().Update(entity);
        }

       

        
    }
}
