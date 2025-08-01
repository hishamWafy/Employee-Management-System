using Manage.BLL.Common.Services.Attachments;
using Manage.BLL.Models.Employees;
using Manage.DAL.Entities;
using Manage.DAL.Repositories.Employees;
using Manage.DAL.Unit_Of_Work;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.BLL.Services.Employees
{
    public class EmployeeServices : IEmployeeServices
    {
       // private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttachmentService _attachmentService;

        public EmployeeServices( IUnitOfWork unitOfWork ,IAttachmentService attachmentService)
        {   
            _unitOfWork = unitOfWork;
            _attachmentService = attachmentService;
        }
        public async Task<IEnumerable<EmployeeDTO>> GetAllEmployeesAsync(string search)
        {
            var employees = await _unitOfWork.EmployeeRepository
                .GetAllAsIQueryable()
                .Where(E => !E.IsDeleted && ( string.IsNullOrEmpty(search) || E.Name.ToLower().Contains(search.ToLower()) ))
                .Include(E => E.Department)
                .Select(employee => new EmployeeDTO()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    IsActive = employee.IsActive,
                    Salary = employee.Salary,
                    Email = employee.Email,
                    Gender = employee.Gender.ToString(),
                    EmployeeType = employee.EmployeeType.ToString(),
                    Department = employee.Department.Name,
                    ImageUrl = employee.ImageUrl,

                }).ToListAsync();
            

            return employees;

        }
        
        public async Task<DetailsEmployeeDTO?> GetEmployeeByIdAsync(int id)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(id);

            if (employee is { })
                return new DetailsEmployeeDTO()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Address = employee.Address,
                    IsActive = employee.IsActive,
                    Salary = employee.Salary,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    HiringDate = employee.HiringDate,
                    Gender = employee.Gender,
                    EmployeeType = employee.EmployeeType,
                    Department = employee?.Department?.Name,
                    ImageUrl = employee.ImageUrl,
                };

            return null;
        }

        public async Task<int> CreateEmployeeAsync(CreateEmployeeDTO employeeDto)
        {
            

            var employee = new Employee()
            {
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                Address = employeeDto.Address,
                IsActive = employeeDto.IsActive,
                Salary = employeeDto.Salary,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                DepartmentId = employeeDto.DepartmentId,
                CreatedBy = 1,
                LastModifiedBy = 1,
                LastModifiyAt = DateTime.Now,
                //ImageUrl = employeeDto.ImageUrl,
            };

            if(employeeDto.ImageUrl is not null)
            {
                // Upload the image and set the ImageUrl property
                employee.ImageUrl = await _attachmentService.UploadAsync(employeeDto.ImageUrl, "Images");
            }

            _unitOfWork.EmployeeRepository.Add(employee);
            return await _unitOfWork.CompleteAsync(); // Save changes to the database
        }

        public async Task<int> UpdateEmployeeAsync(UpdateEmplooyeeDTO employeeDto)
        {
            var employee = new Employee()
            {
                Id = employeeDto.Id,
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                Address = employeeDto.Address,
                IsActive = employeeDto.IsActive,
                Salary = employeeDto.Salary,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                DepartmentId = employeeDto.DepartmentId,
                CreatedBy = 1,
                LastModifiedBy = 1,
                LastModifiyAt = DateTime.Now,
            };

           
             _unitOfWork.EmployeeRepository.Update(employee);

            return await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employeerepo = _unitOfWork.EmployeeRepository;

            var employee = await employeerepo.GetByIdAsync(id);
            if(employee is { })
            {
                employeerepo.Delete(employee) ;
            }
            return await _unitOfWork.CompleteAsync() > 0 ;
        }

        
    }
}
