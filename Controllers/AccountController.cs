using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using LoginFPTBook.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FPTBook.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _db = db;
        }
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Index()
        {
            IEnumerable<ApplicationUser> lstAccount = _db.Users.ToList();
            int count = _db.Users.Count();
            ViewData["count"] = count;
            IList<string> lstRoles = new List<string>();
            foreach (var item in lstAccount)
            {
                var role = await _userManager.GetRolesAsync(item);
                lstRoles.Add(role[0]);
            }
            ViewData["role"] = lstRoles.ToList();

            return View(lstAccount);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> updateAccount(string id)
        {
            if (ModelState.IsValid)
            {
                var user = _db.Users.Find(id);

                var role = await _userManager.GetRolesAsync(user);

                TempData["role"] = role[0];
                return View(user);
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult updateAccount(int status, string idUser)
        {
            if (ModelState.IsValid)
            {
                var user = _db.Users.Find(idUser);
                user.User_Status = status;
                if (status == 0)
                {
                    var dateBanEnd = Convert.ToDateTime("1/1/3000");
                    _userManager.SetLockoutEndDateAsync(user, dateBanEnd);
                }
                else
                {
                    _userManager.SetLockoutEndDateAsync(user, null);
                }
                _db.Users.Update(user);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("updateAccount", new{id = idUser});
        }
        [Authorize(Roles = "Admin")]
        public IActionResult createOwnerAccount()
        {
            ViewData["roleAdmin"] = "Admin";
            return RedirectToPage("/Account/Register", new { area = "Identity" });
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SearchUser(string search)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<ApplicationUser> lstAccount = _db.Users.Where(u => u.Email == search || u.Email.Contains(search) || u.Email.StartsWith(search) || u.Email.EndsWith(search)).ToList();
                int count = lstAccount.Count();
                ViewData["count"] = count;

                IList<string> lstRoles = new List<string>();
                foreach (var item in lstAccount)
                {
                    var role = await _userManager.GetRolesAsync(item);
                    lstRoles.Add(role[0]);
                }
                ViewData["role"] = lstRoles.ToList();

                return View(lstAccount);
            }
            return RedirectToAction("Index");
        }

    }
}