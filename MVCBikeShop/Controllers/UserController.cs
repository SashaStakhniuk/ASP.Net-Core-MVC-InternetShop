using MVCBikeShop.Models;
using MVCBikeShop.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace BiicycleShopDb.Controllers
{
    public class UserController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;
        //private IHttpContextAccessor httpContextAccessor;

        public UserController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager/*, IHttpContextAccessor httpContextAccessor*/)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        [Authorize(Roles="MainAdmin")]
        //public IActionResult Index() => View(roleManager.Roles.ToList());
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await userManager.FindByNameAsync(User.Identity.Name);
                var userRoles = await userManager.GetRolesAsync(user);
                return View(new UserViewModel {
                    CurentEditor=userRoles.FirstOrDefault(),
                    AllRoles = await roleManager.Roles.ToListAsync(),
                    UserManager = userManager
                });

            }
            return View(new UserViewModel
            {
                AllRoles = await roleManager.Roles.ToListAsync(),
                UserManager = userManager
            });
        }
        [Authorize(Roles = "MainAdmin")]

        public IActionResult CreateUser()
        {
            var allRoles = roleManager.Roles.ToList();
            return View(new UserViewModel { AllRoles = allRoles });
        }
        //public IActionResult CreateUser()
        //{
        //    return View(new RegisterViewModel());
        //}

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateUser(UserViewModel model)
        {
            if(ModelState.IsValid)
            {
                User newUser = new User { Email = model.UserEmail, UserName = model.UserName, LastName = model.LastName};
                IdentityResult result = await userManager.CreateAsync(newUser);
                
                await userManager.AddPasswordAsync(newUser, model.Password);
                if (result.Succeeded)
                {
                    var userRoles = await userManager.GetRolesAsync(newUser);
                    // получаем список ролей, которые были добавлены
                    var addedRoles = model.UserRoles.Except(userRoles);
                    // получаем роли, которые были удалены
                    var removedRoles = userRoles.Except(model.UserRoles);

                    await userManager.AddToRolesAsync(newUser, addedRoles);

                    await userManager.RemoveFromRolesAsync(newUser, removedRoles);

                    return RedirectToAction("Index", "User");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return RedirectToAction("CreateUser", "User");
                }
            }
            else
            {
                var allRoles = roleManager.Roles.ToList();
               // return View(model);
                return View(new UserViewModel { UserName=model.UserName,LastName = model.LastName, UserEmail = model.UserEmail,Password=model.Password,AllRoles = allRoles });            
            }
           
        }
        [Authorize(Roles = "MainAdmin")]

        public async Task<IActionResult> DeleteUser(string Id)
        {
            User user =  userManager.Users.FirstOrDefault(x => x.Id == Id);

            IdentityResult result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "User");
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> UserAccount()
        {
            //string currentUserId = "";

            if (User.Identity.IsAuthenticated)
            {
                var user = await userManager.FindByNameAsync(User.Identity.Name);
                var userRoles = await userManager.GetRolesAsync(user);
                return View(new UserViewModel { UserId = user.Id, UserName=user.UserName, LastName = user.LastName, UserEmail = user.Email,UserRoles=userRoles});

            }
            //if (User.Identity.IsAuthenticated)
            //{
            //    currentUserId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //}
            //if (!string.IsNullOrEmpty(currentUserId))
            //{
            //    return View(new UserViewModel { UserId = currentUserId, UserManager = userManager });
            //}
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public async Task<IActionResult> EditUser(string userId,string role="User")
        {
                User user = await userManager.FindByIdAsync(userId);
                UserViewModel model = new UserViewModel();
                if (user != null)
                {
                    // получем список ролей пользователя
                    if(role == "MainAdmin")
                    {
                        var userRoles = await userManager.GetRolesAsync(user);
                        var allRoles = roleManager.Roles.ToList();
                        //var token=await userManager.GeneratePasswordResetTokenAsync(user);
                        //UserViewModel model = new UserViewModel
                        //{
                        //    UserId = user.Id,
                        //    UserName = user.UserName,
                        //    LastName = user.LastName,
                        //    UserEmail = user.Email,
                        //    //ResetPasswordToken=token,
                        //    //Password =  ,
                        //    UserRoles = userRoles,
                        //    AllRoles = allRoles,
                        //    UserManager = userManager
                        //};
                        model.UserId = user.Id;
                        model.UserName = user.UserName;
                        model.LastName = user.LastName;
                        model.UserEmail = user.Email;
                        //model.//ResetPasswordToken=token,
                        //model.//Password =  ,
                        model.UserRoles = userRoles;
                        model.AllRoles = allRoles;
                        model.UserManager = userManager;
                        model.CurentEditor = "MainAdmin";
                    }
                    else
                    {
                        model.UserId = user.Id;
                        model.UserName = user.UserName;
                        model.LastName = user.LastName;
                        model.UserEmail = user.Email;
                        model.UserManager = userManager;
                    }

                    return View(model);
                }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(UserViewModel model)
        {
            User user = await userManager.FindByIdAsync(model.UserId);
            if (user != null)
            {
                user.UserName = model.UserName;
                user.LastName = model.LastName;
                user.Email = model.UserEmail;
                //var token = await userManager.GeneratePasswordResetTokenAsync(user);
                //await userManager.ChangePasswordAsync(user,token, model.Password);
                if(!String.IsNullOrEmpty(model.Password))
                {
                    await userManager.RemovePasswordAsync(user);
                    await userManager.AddPasswordAsync(user, model.Password);
                }
                var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                    var userRoles = await userManager.GetRolesAsync(user);
                    // ролі, як були додані
                    var addedRoles = model.UserRoles.Except(userRoles);
                    // ролі, як були видалені
                    var removedRoles = userRoles.Except(model.UserRoles);

                    await userManager.AddToRolesAsync(user, addedRoles);

                    await userManager.RemoveFromRolesAsync(user, removedRoles);
                    if(model.CurentEditor=="MainAdmin")
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("UserAccount","User");
                    }
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }
            }
            return NotFound();
        }
    }
}
