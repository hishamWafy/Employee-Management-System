using Humanizer;
using Manage.BLL.Common.Services.Attachments;
using Manage.BLL.Models.Dpartments;
using Manage.BLL.Models.Employees;
using Manage.BLL.Services.Departments;
using Manage.BLL.Services.Employees;
using Manage.DAL.Entities;
using Manage.PL.ViewModels.Employees;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;

namespace Manage.PL.Controllers
{
    public class EmployeeController : Controller
    {
        #region Services
        private readonly IEmployeeServices _employeeServices;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly IAttachmentService _attachmentService;

        public EmployeeController(
            IEmployeeServices employeeServices,
            ILogger<EmployeeController> logger, 
            IWebHostEnvironment environment,
             IAttachmentService attachmentService)

        {
            _employeeServices = employeeServices;
            _logger = logger;
            _environment = environment;
            _attachmentService = attachmentService;
        }
        #endregion

        #region Index
        [HttpGet] // GET: Employee
        public async Task<IActionResult> Index(string search)
        {

            var employees = await _employeeServices.GetAllEmployeesAsync(search );

           
            return View(employees);
        }
        #endregion

        #region Create
        [HttpGet] // GET: Employee/Create
        public async Task<IActionResult> Create([FromServices] IDepartmentServices departmentServices)
        {
            // to Get Departments at Department filed
            var departments = await departmentServices.GetAllDepartmentsAsync()/*.ToListAsync()*/;
            ViewBag.Departments = new SelectList(departments, "Id", "Name");

            return View();
        }

        [HttpPost] // POST: Employee/Create
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEmployeeDTO employee)
        {
            if (!ModelState.IsValid)
                return View(employee);

            string message = "Failed to create employee";

            try
            {
                var created = await _employeeServices.CreateEmployeeAsync(employee)>0;
                if (created)
                {
                    TempData["Message"] = "Employee was created successfully!";
                }
                else
                {
                    TempData["Message"] = "Employee creation failed!";
                }
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                message = _environment.IsDevelopment() ? "An error occurred: " + ex.Message : "Failed to create employee";
            }

            ModelState.AddModelError("", message);
            return View(employee);
        }

        #endregion

        #region Details
        [HttpGet] // GET: Employee/Details/{id}
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return BadRequest();

            var employee = await _employeeServices.GetEmployeeByIdAsync(id.Value);
            if (employee is null)
                return NotFound();

            return View(employee);
        }
        #endregion

        #region Edit
        [HttpGet] // GET: Employee/Edit/{id}
        public async Task<IActionResult> Edit(int? id ,[FromServices] IDepartmentServices departmentServices)
        {
            var departments = await departmentServices.GetAllDepartmentsAsync();
            ViewBag.Departments = new SelectList(departments, "Id", "Name");

            if (id is null)
                return BadRequest();

            var employee = await _employeeServices.GetEmployeeByIdAsync(id.Value);
            if (employee is null)
                return NotFound();

            var model = new UpdateEmplooyeeDTO()
            {
                Name = employee.Name,
                Address = employee.Address,
                Email = employee.Email,
                Age = employee.Age,
                Salary = employee.Salary,
                PhoneNumber = employee.PhoneNumber,
                IsActive = employee.IsActive,

                HiringDate = employee.HiringDate,
                EmployeeType = employee.EmployeeType,
                Gender = employee.Gender
                
            };

            return View(model);
        }

        [HttpPost] // POST: Employee/Edit/{id}
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, UpdateEmplooyeeDTO employee)
        {
            if (!ModelState.IsValid)
                return View(employee);
            var message = string.Empty;

            try
            {
               

                var result = await _employeeServices.UpdateEmployeeAsync(employee) ;
                if (result > 0)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                 message = _environment.IsDevelopment() ? "An error occurred: " + ex.Message : "Failed to update employee";
            }

            ModelState.AddModelError("", message);
            return View(employee);
        }
        #endregion

        #region Delete
        [HttpGet] // GET: Employee/Delete/{id}
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return BadRequest();

            var employee = await _employeeServices.GetEmployeeByIdAsync(id.Value);
            if (employee is null)
                return NotFound();

            return View(employee);
        }

        [HttpPost] // POST: Employee/Delete/{id}
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            string message = "Failed to delete employee";

            try
            {
                var delete = await _employeeServices.DeleteEmployeeAsync(id);
                if (delete)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                message = _environment.IsDevelopment() ? "An error occurred: " + ex.Message : "Failed to delete employee";
            }

            ModelState.AddModelError("", message);
            return RedirectToAction(nameof(Index));
        }

        #endregion


        #region Search
        [HttpGet]
        public async Task<IActionResult> Search(string term)
        {
            try
            {
                IEnumerable<EmployeeDTO> results;
                if (string.IsNullOrWhiteSpace(term))
                {
                    results = await _employeeServices.GetAllEmployeesAsync(string.Empty);
                }
                else
                {
                    term = term.Trim();
                    if (term.Length > 100)
                    {
                        results = new List<EmployeeDTO>();
                    }
                    else
                    {
                        var employees = await _employeeServices.GetAllEmployeesAsync(term);
                        results = employees
                            .Where(e => e.Name.Contains(term, StringComparison.OrdinalIgnoreCase))
                            .Take(50)
                            .ToList();

                    }
                }

                return PartialView("/Views/Employee/Partial/_EmployeesTablePartial.cshtml", results);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error in employee search");
                var fallback = _employeeServices.GetAllEmployeesAsync(string.Empty);
                return PartialView("/Views/Employee/Partial/_EmployeesTablePartial.cshtml", fallback);
            }
        }



        #endregion


    }



}
