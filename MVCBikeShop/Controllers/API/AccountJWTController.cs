using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MVCBikeShop.Config;
using MVCBikeShop.Models;
using MVCBikeShop.Models.ViewModels;

namespace MVCBikeShop.Controllers.API
{

    //public class User
    //{
    //    public string Login { get; set; }
    //    public string Email { get; set; }
    //    public string Password { get; set; }
    //    public string Role { get; set; }

    //}
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountJWTController : Controller
    {

        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        public AccountJWTController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

       

        [HttpPost]
        public async Task<IActionResult> Token(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                //var result = await signInManager.PasswordSignInAsync(user.UserName, model.Password, false, false);
                if (user!=null)
                {
                    var identity = await GetIdentity(user.Email, model.Password);
                    if (identity == null)
                    {
                        return BadRequest(new { error = "Invalid login or password" });
                    }
                    var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSURER,
                        audience: AuthOptions.AUDIENCE,
                        claims: identity.Claims,
                        expires: DateTime.Now.AddMinutes(AuthOptions.LIFETIME),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                        );
                    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                    return Json(new
                    {
                        access_token = encodedJwt
                    });
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login or password");
                }
            }
            return View(model);
        }
        private async Task<ClaimsIdentity> GetIdentity(string email, string password)
        {
            var user = await userManager.FindByEmailAsync(email);
            var result = await signInManager.PasswordSignInAsync(user.UserName, password, false, false);
            if (result.Succeeded)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType,user.Email),
                    new Claim(ClaimsIdentity.DefaultNameClaimType, password)
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            else
            {
                ModelState.AddModelError("", "Invalid login or password");
            }
            return null;
        }
        //private User mainAdmin = new User
        //{
        //    Login = "admin",
        //    Password = "admin",
        //    Role = "MainAdmin"
        //}; 

        //[HttpPost]
        //public IActionResult Token(User user)
        //{
        //    var identity = GetIdentity(user.Login, user.Password);
        //    if(identity==null)
        //    {
        //        return BadRequest(new { error = "Invalid login or password" });
        //    }
        //    var jwt = new JwtSecurityToken(
        //        issuer:AuthOptions.ISSURER,
        //        audience:AuthOptions.AUDIENCE,
        //        claims: identity.Claims,
        //        expires: DateTime.Now.AddMinutes(AuthOptions.LIFETIME),
        //        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),SecurityAlgorithms.HmacSha256)
        //        );
        //    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
        //    return Json(new
        //    {
        //        access_token=encodedJwt
        //    });
        //}

        //private async Task<ClaimsIdentity> GetIdentity(string login,string password)
        //{
        //    var user = await userManager.FindByEmailAsync(login);
        //    var result = await signInManager.PasswordSignInAsync(user.UserName, password, false, false);
        //    if (result.Succeeded)
        //    {
        //        var claims = new List<Claim>
        //        {
        //            new Claim(ClaimsIdentity.DefaultNameClaimType,user.Email),
        //            new Claim(ClaimsIdentity.DefaultNameClaimType, password)
        //        };
        //        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        //        return claimsIdentity;
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", "Invalid login or password");
        //    }
        //    //if (login==user.Email /*&& password== mainAdmin.Password*/)
        //    //{
        //    //    var claims = new List<Claim>
        //    //    {
        //    //        new Claim(ClaimsIdentity.DefaultNameClaimType,user.Email)

        //    //        //new Claim(ClaimsIdentity.DefaultNameClaimType, mainAdmin.Password)
        //    //    };
        //    //    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        //    //    return claimsIdentity;
        //    //}
        //    return null;
        //}
    }
}