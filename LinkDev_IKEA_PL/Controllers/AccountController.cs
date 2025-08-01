using Manage.DAL.Identity;
using Manage.PL.ViewModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Manage.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(
            UserManager<ApplicationUser> userManager ,
            SignInManager<ApplicationUser> signInManager)
        {
           _userManager = userManager;
            _signInManager = signInManager;
        }

        #region SignUp
        // GET: Account/SignUp
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        // POST: Account/SignUp
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model state.");
            }


            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user is { })
            {
                ModelState.AddModelError(nameof(SignUpViewModel.UserName), "this user name is already Used  ");
                return View(model);

            }


            user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
                IsAgree = model.ISAgree,
            };

            var result = await _userManager.CreateAsync(user, model.Password);


            if (result.Succeeded)
            {
                return RedirectToAction("SignIn");

            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }


            return View(model);


        }

        #endregion


        [HttpGet] // GET: Account/SignIn
        public  IActionResult SignIn()
        {
            return View();
        }


        [HttpPost] // GET: Account/SignIn
        public async Task<IActionResult> SignIn(SignInViewModel model)
         {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model state.");
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is { })
            {
                var flag = await _userManager.CheckPasswordAsync(user , model.Password);
                if (flag)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);
                
                    if(result.IsNotAllowed)
                        ModelState.AddModelError(string.Empty, "Your account is not confirmed yet!!.");

                    if(result.IsLockedOut)
                        ModelState.AddModelError(string.Empty, "Your account is locked out!!.");

                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                    }





                }

            }
           
              ModelState.AddModelError(string.Empty, "Invalid login attempt. Please check your credentials and try again.");


            return View(model);




        }



    }
}
