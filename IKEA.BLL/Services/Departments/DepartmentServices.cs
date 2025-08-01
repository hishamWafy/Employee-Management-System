using Manage.BLL.Models.Dpartments;
using Manage.DAL.Entities;
using Manage.DAL.Repositories.Departments;
using Manage.DAL.Unit_Of_Work;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.BLL.Services.Departments
{
    public class DepartmentServices : IDepartmentServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DepartmentDTO>> GetAllDepartmentsAsync()
        {
            var departments = await _unitOfWork.DepartmentRepository
                .GetAllAsIQueryable()
                .Where(D => !D.IsDeleted).Select(department => new DepartmentDTO
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                CreateDate = department.CreateDate
            }).AsNoTracking().ToListAsync();
           
               return departments;
        }
        public async Task<DepartmentDetailsDTO?> GetDepartmentByIdAsync(int id)
        {
            var department = await _unitOfWork.DepartmentRepository.GetByIdAsync(id);

            if (department is not null)
            {
                return new DepartmentDetailsDTO()
                {
                    Id = department.Id,
                    Name = department.Name,
                    Code = department.Code,
                    Description = department.Description,
                    CreateDate = department.CreateDate,
                    CreatedBy = department.CreatedBy,
                    CreatedAt = department.CreatedAt,
                    LastModifiedBy = department.LastModifiedBy,
                    LastModifiyAt = department.LastModifiyAt,
                };
            }

            return null;
        }
        
        public async Task<int> CreateDepartmentAsync(CreatedDepartmentDTO department)
        {
            var createDepartment = new Department()
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                CreateDate = DateTime.UtcNow,
                CreatedBy = 1,
                CreatedAt = DateTime.UtcNow,
                LastModifiedBy = 1,
                LastModifiyAt = DateTime.UtcNow,
                IsDeleted = false
            };

          _unitOfWork.DepartmentRepository.Add(createDepartment);

            return await _unitOfWork.CompleteAsync();
        }

        public async Task<int> UpdateDepartmentAsync(UpdateDepartmentDTO department)
        {
            var Update = new Department()
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                CreateDate = department.CreateDate,
                CreatedBy = 1,
                CreatedAt = DateTime.UtcNow,
                LastModifiedBy = 1,
                LastModifiyAt = DateTime.UtcNow,
               
            };

             _unitOfWork.DepartmentRepository.Update(Update);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            var departmentRepo = _unitOfWork.DepartmentRepository;

           var department = await departmentRepo.GetByIdAsync(id);
            if (department != null)
            {
                departmentRepo.Delete(department);
            }

            return await _unitOfWork.CompleteAsync() > 0;
        }

    }
}
