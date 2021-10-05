using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCBikeShop.Models;
using MVCBikeShop.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVCBikeShop.Controllers
{
    public class AccountController : Controller
    {
        //BikeContext context;
        //var roleStore = new RoleStore<IdentityRole>(context);
        //var roleMngr = new RoleManager<IdentityRole>(roleStore);

        //var roles = roleMngr.Roles.ToList();


        private readonly RoleManager<IdentityRole> roleManager;

        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }
        public IActionResult Login( string returnUrl=null)
        {
            //LoginViewModel model=new LoginViewModel();
            return View(new LoginViewModel { ReturnUrl=returnUrl});
            //return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                var result = await signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if(!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index","Home");
                    }
                }
                else
                {                   
                        ModelState.AddModelError("","Invalid login or password");                   
                }
            }
            return View(model);
        }
        public IActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel();
            return View(model);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //User user = await context.Users.FirstOrDefaultAsync(x => x.Email == model.Email && x.Password == model.Password);
                var roles = await roleManager.Roles.ToListAsync();
                if(roles.Count()==0)
                {
                    await roleManager.CreateAsync(new IdentityRole("MainAdmin"));
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                    await roleManager.CreateAsync(new IdentityRole("User"));
                }

                User user = new User { Email = model.Email, UserName = model.UserName, LastName = model.LastName };

                IdentityResult result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    List<string> role = new List<string>();

                    if (user.Email=="MainAdmin@gmail.com" && user.UserName=="Main"&&user.LastName=="Admin")
                    {
                        role.Add("MainAdmin");
                    }
                    else
                    {
                        role.Add("User");
                    }
                   
                    await userManager.AddToRolesAsync(user,role);
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }
        public IActionResult ModalLogin(string returnUrl = null)
        {
            //LoginViewModel model=new LoginViewModel();
            return View(new LoginViewModel { ReturnUrl = returnUrl });
            //return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ModalLogin(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                var result = await signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login or password");
                }
            }
            return View(model);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
                                                                                           