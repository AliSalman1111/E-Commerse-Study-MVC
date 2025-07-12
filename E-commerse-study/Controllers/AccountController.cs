using System.Threading.Tasks;
using E_commerse_study.Models;
using E_commerse_study.Static_Data;
using E_commerse_study.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace E_commerse_study.Controllers
{
    public class AccountController : Controller
    {
        UserManager<ApplicationUser> UserManager;
        SignInManager<ApplicationUser> SignInManager;
        RoleManager<IdentityRole> RoleManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {

              UserManager=userManager;
              SignInManager=signInManager;
            RoleManager=roleManager;

            }

        public async Task<IActionResult> Register()
        {
            if (RoleManager.Roles.IsNullOrEmpty())
            {
              await  RoleManager.CreateAsync(new(SD.AdminRole));
                await RoleManager.CreateAsync(new(SD.CompanyRole));
                await RoleManager.CreateAsync(new(SD.CustomerRole));
            }
            ApplicatinUserVM userVM =new ApplicatinUserVM();    
            return View(userVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(ApplicatinUserVM userVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = new()
                {
                    UserName = userVM.Name,
                    Email = userVM.Email,
                    Address = userVM.Address
                    // PasswordHash=userVM.Password


                };

                var result = await UserManager.CreateAsync(applicationUser, userVM.Password);

                // return View();
                if (result.Succeeded)
                {
                  await  UserManager.AddToRoleAsync(applicationUser, SD.CustomerRole);
                    await SignInManager.SignInAsync(applicationUser, false);
                    return   RedirectToAction("Index", "Home");
                }
                else
                {

                    ModelState.AddModelError("Password", "eror");
                }
            }
            return View(userVM);
        }

        public IActionResult Login()
        {
            loginVM loginVM = new loginVM ();
            return View(loginVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Login(loginVM loginVM)
        {
            if (ModelState.IsValid) {
              //1.chrck user name 
                 var applicationuser =await UserManager.FindByNameAsync(loginVM.UserName);

                if (applicationuser != null) {
                    //2. check password
                var result=   await  UserManager.CheckPasswordAsync(applicationuser, loginVM.passward);
                    if (result)
                    {
                        // 3login
                       await SignInManager.SignInAsync(applicationuser, loginVM.Remembermy);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("passward", "Invalid passward");

                    }
                }
                else
                {

                    ModelState.AddModelError("UserName", "Invalid user name");
                }
            
            }
           
            return View(loginVM);
        }

        public IActionResult Logout()
        {
            SignInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
