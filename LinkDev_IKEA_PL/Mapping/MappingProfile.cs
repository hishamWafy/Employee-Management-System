namespace LinkDev_Manage_PL.Mapping
    
{
using AutoMapper;
    using Manage.BLL.Models.Dpartments;
    using Manage.DAL.Entities;
    using Manage.DAL.Identity;
    using Manage.PL.ViewModels;
    using Manage.PL.ViewModels.Departments;
    using Manage.PL.ViewModels.Employees;
    using Manage.PL.ViewModels.Identity;
    using Microsoft.AspNetCore.Identity;

    public class MappingProfile : Profile
 {

        public MappingProfile()
        {
            #region Employee


            #endregion
            CreateMap<EmployeeEditViewModel, Employee>().ReverseMap();
            
            CreateMap<ApplicationUser, UserViewModel>().ReverseMap();
            CreateMap<RoleViewModel, IdentityRole>()
                .ForMember(d => d.Name, O => O.MapFrom(s => s.RoleName))
                .ReverseMap();


            #region Department

            CreateMap<DepartmentViewModel, Department>().ReverseMap();
            CreateMap<DepartmentDetailsDTO, DepartmentViewModel>();

            CreateMap<DepartmentViewModel, UpdateDepartmentDTO>();
            CreateMap<DepartmentViewModel, CreatedDepartmentDTO>();


            #endregion


        }










    }





}
