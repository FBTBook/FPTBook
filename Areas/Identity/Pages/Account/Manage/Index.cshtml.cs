// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using LoginFPTBook.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoginFPTBook.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            public string User_Fullname { get; set; }
            public DateTime User_Birthdate { get; set; }
            public string User_Address { get; set; }           
            public string User_Gender { get; set; } 
            
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            
            Username = userName;
            Input = new InputModel
            {
                User_Fullname = user.User_Fullname,
                User_Birthdate = user.User_Birthdate,
                User_Address = user.User_Address,
                User_Gender = user.User_Gender,            
                PhoneNumber = phoneNumber,
                Email = email,
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            if (Input.User_Fullname != user.User_Fullname)
            {
                user.User_Fullname = Input.User_Fullname;
                await _userManager.UpdateAsync(user);
            }

            if (Input.User_Birthdate != user.User_Birthdate)
            {
                user.User_Birthdate = Input.User_Birthdate;
                await _userManager.UpdateAsync(user);
            }

            if (Input.User_Address != user.User_Address)
            {
                user.User_Address = Input.User_Address;
                await _userManager.UpdateAsync(user);
            }

            if (Input.User_Gender != user.User_Gender)
            {
                user.User_Gender = Input.User_Gender;
                await _userManager.UpdateAsync(user);
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            var username = await _userManager.GetUserNameAsync(user);
            if (Username != username)
            {
                await _userManager.SetUserNameAsync(user, Username);
            }

            if (Input.Email != user.Email)
            {
                user.Email = Input.Email;
                await _userManager.UpdateAsync(user);
            }

            // await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
