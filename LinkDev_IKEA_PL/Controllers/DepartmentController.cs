using AutoMapper;
using Manage.BLL.Models.Dpartments;
using Manage.BLL.Services.Departments;
using Manage.PL.ViewModels.Departments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;

namespace Manage.PL.Controllers
{
    public class DepartmentController : Controller
    {

        #region Services
        private readonly IDepartmentServices _departmentServices;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        public DepartmentController
            (
            IDepartmentServices departmentServices,
            ILogger<DepartmentController> logger,
            IMapper mapper,
            IWebHostEnvironment environment
            )
        {
            _departmentServices = departmentServices;
            _logger = logger;
            _mapper = mapper;
            _environment = environment;
        }
        #endregion

        #region Index
        [HttpGet] // GET: Department
        public async Task<IActionResult> Index()
        {
            var departments = await _departmentServices.GetAllDepartmentsAsync();

            return View(departments);
        }
        #endregion

        #region Create
        [HttpGet] // GET: Department
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] // POST: Department
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentViewModel department)
        {
            if (!ModelState.IsValid)
            {
                return View(department);
            }

            var message = "Failed to create department";

            try
            {

               

                var model = _mapper.Map<CreatedDepartmentDTO>(department);

                var result = await _departmentServices.CreateDepartmentAsync(model);

                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", message);
                    return View(department);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                message = _environment.IsDevelopment() ? ex.Message : "An error occurred while creating department";

            }

            ModelState.AddModelError(string.Empty, message);
            return View(department);

        }

        #endregion

        #region Details
        [HttpGet] // GET: Department
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }

            var department = await _departmentServices.GetDepartmentByIdAsync(id.Value);

            if (department is null)
            {
                return NotFound();
            }

            return View(department);
        }

        #endregion

        #region Edit
        [HttpGet] // GET: Department/Edit/{id}
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }

            var department = await _departmentServices.GetDepartmentByIdAsync(id.Value);
            if (department is null)
            {
                return NotFound();
            }
            var departmentVM = _mapper.Map<DepartmentDetailsDTO , DepartmentViewModel>(department);
            

            return View(departmentVM);
        }


        [HttpPost] // POST: Department/Edit/{id}
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, DepartmentViewModel department)
        {
            if (!ModelState.IsValid)
            {
                return View(department);
            }

            var message = "Failed to update department";

            try
            {
               

                var departmentDTO = _mapper.Map<UpdateDepartmentDTO>(department);

                var result = await _departmentServices.UpdateDepartmentAsync(departmentDTO) > 0;
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }

                message = "An error has occurred during updating the department :(";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                message = _environment.IsDevelopment() ? ex.Message : "An error occurred while updating department";
            }

            ModelState.AddModelError(string.Empty, message);
            return View(department);
        }

        #endregion

        #region Delete
        [HttpGet] //  GET:/Delete/id
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return BadRequest();

            var department = await _departmentServices.GetDepartmentByIdAsync(id.Value);

            if (department is null)
                return NotFound();

            return View(department);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var message = string.Empty;
            try
            {
                var delete = await _departmentServices.DeleteDepartmentAsync(id);

                if (delete)
                    return RedirectToAction(nameof(Index));

                message = "an error has occured during deleting the department :(";

            }
            catch (Exception ex)
            {
                // 1. Log Exception 
                _logger.LogError(ex, ex.Message);

                // 2. Set Message
                message = _environment.IsDevelopment() ? ex.Message : "an error has occured during deleting the department :(";




            }

            ModelState.AddModelError(string.Empty, message);
            return RedirectToAction(nameof(Index));

        } 
        #endregion

    }



}
