using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using LoginFPTBook.Data;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
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

        public async Task<ActionResult> Index()
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
                lstRoles = await _userManager.GetRolesAsync(item);
            }
            ViewData["role"] = lstRoles.ToList();

            return View(lstAccount);
            // return Json(ViewData["role"]);
        }

        // public IActionResult ActionName()
        // {
        //     string query = "Select u.id, u.User_Fullname, r.name as Role From AspNetUsers u, AspNetRoles r, AspNetUserRoles ur Where u.id = ur.UserId and r.Id = ur.RoleId";
        //     var LstUsers = _db.Users.FromSqlRaw(query).ToList();
        //     return Ok(LstUsers);
        // }

        public IActionResult updateAccount()
        {
            return View();
        }
        public IActionResult createOwnerAccount()
        {
            return View();
        }
        public async Task<IActionResult> SearchUser(string search)
        {
            IEnumerable<ApplicationUser> lstAccount = _db.Users.Where(u => u.Email == search || u.Email.Contains(search) || u.Email.StartsWith(search) || u.Email.EndsWith(search)).ToList();
            int count = _db.Users.Count();
            ViewData["count"] = count;
            IList<string> lstRoles = new List<string>();
            foreach (var item in lstAccount)
            {
                lstRoles = await _userManager.GetRolesAsync(item);
            }
            ViewData["role"] = lstRoles.ToList();

            return View(lstAccount);
        }
        
    }
}