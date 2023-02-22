using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using LoginFPTBook.Data;
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

        public  async Task<ActionResult> Index()
        {
            // ViewData["Role"] = _db.Roles.ToList();
            // ViewData["UserRole"] = _db.UserRoles.ToList();

            // IEnumerable<IdentityRole> lstRole = _role.Roles;
            IEnumerable<ApplicationUser> lstAccount = _db.Users.ToList();
            int count = _db.Users.Count();
            ViewData["count"] = count;
            IList<string> lstRoles = new List<string>();
            foreach (var item in lstAccount)
            {
                lstRoles =  await _userManager.GetRolesAsync(item);
            }
            ViewData["role"] = lstRoles.ToList();
            
            return View(lstAccount);
            // return Json(ViewData["role"]);
        }

        public IActionResult updateAccount()
        {
            return View();
        }
        
        public IActionResult createOwnerAccount()
        {
            return View();
        }
        
    }
}