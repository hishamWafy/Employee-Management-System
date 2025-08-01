using Manage.BLL.Common.Services.Attachments;
using Manage.BLL.Services.Departments;
using Manage.BLL.Services.Employees;
using Manage.DAL.Data.Contexts;
using Manage.DAL.Identity;
using Manage.DAL.Repositories.Departments;
using Manage.DAL.Repositories.Employees;
using Manage.DAL.Unit_Of_Work;
using LinkDev_Manage_PL.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Manage.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
              var builder = WebApplication.CreateBuilder(args);
               
             #region Configure Services
            builder.Services.AddControllersWithViews();

            builder.Services.AddSession();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                
                options.UseLazyLoadingProxies()
                .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            //builder.Services.AddScoped<IDepartmentReopsitory, DepartmentRepository>();
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            builder.Services.AddScoped<IUnitOfWork, UintOfWork>();

            builder.Services.AddScoped<IDepartmentServices , DepartmentServices>();
            builder.Services.AddScoped<IEmployeeServices , EmployeeServices>();

            builder.Services.AddTransient<IAttachmentService, AttachmentService>();

            //builder.Services.AddAutoMapper(typeof(MappingProfile));
            //builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfile()));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredUniqueChars = 1;


                options.User.RequireUniqueEmail = true;
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(5);


            })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/SignIn";
                options.AccessDeniedPath = "/Home/Error";
                options.ExpireTimeSpan = TimeSpan.FromDays(5);
                options.SlidingExpiration = true; // This will reset the expiration time on each request
            });
                


            #endregion

            var app = builder.Build();

            #region Configure Kestrel Middle wares

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Redirect from HTTP to HTTPS
            app.UseHttpsRedirection();
            // to allow the use files of wwwroot
            app.UseStaticFiles();

            // To read the URL from the browser, it then performs the appropriate action
            app.UseRouting();

            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            #endregion

            app.Run();
        }
    }
}
  