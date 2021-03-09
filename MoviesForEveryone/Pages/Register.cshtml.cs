using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MoviesForEveryone.Models;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace MoviesForEveryone.Pages
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<MFEUser> _signInManager;
        private readonly UserManager<MFEUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;

        //public RegisterModel(
        //  UserManager<MFEUser> userManager,
        //  SignInManager<MFEUser> signInManager,
        //  ILogger<RegisterModel> logger)
        //{
        //    _userManager = userManager;
        //    _signInManager = signInManager;
        //    _logger = logger;
        //}

        //[BindProperty]
        //public InputModel Input { get; set; }

        //public class InputModel
        //{
        //    [Required]
        //    [EmailAddress]
        //    public string Email { get; set; }

        //    [Required]
        //    public string UserName { get; set; }

        //    [Required]
        //    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        //    [DataType(DataType.Password)]
        //    public string Password { get; set; }

        //    [DataType(DataType.Password)]
        //    [Display(Name = "Confirm Password")]
        //    [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        //    public string ConfirmPassword { get; set; }
        //}

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {            
            if (ModelState.IsValid)
            {
                //var user = new MFEUser { UserName = Input.Email, Email = Input.Email};
                //var result = await _userManager.CreateAsync(user, Input.Password);
                //if (result.Succeeded)
                //{
                    
                //}
            }

            return Page();
        }
    }
}
