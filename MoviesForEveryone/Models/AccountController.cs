using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoviesForEveryone.Pages;
using Microsoft.AspNetCore.Authorization;

namespace MoviesForEveryone.Models
{
    public class AccountController : Controller
    {
        private readonly UserManager<MFEUser> _userManager;
        private readonly SignInManager<MFEUser> _signInManager;

        public AccountController(UserManager<MFEUser> userManager, SignInManager<MFEUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    var user = new MFEUser
            //    {
            //        UserName = model.Input.UserName,
            //        Email = model.Input.Email,
            //    };

            //    var result = await _userManager.CreateAsync(user, model.Input.Password);

            //    if (result.Succeeded)
            //    {
            //        await _signInManager.SignInAsync(user, isPersistent: false);

            //        return RedirectToAction("index", "Home");
            //    }

            //    foreach (var error in result.Errors)
            //    {
            //        ModelState.AddModelError("", error.Description);
            //    }

            //    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            //}
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel user)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(user);
        }


    }
}
