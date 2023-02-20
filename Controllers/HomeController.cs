using LoginFPTBook.Data;
using LoginFPTBook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace LoginFPTBook.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            // return Ok(HttpContext.Items["Temp"]);
            return View();
        }

        [Authorize]
        public IActionResult OrderHistory()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var findOrder = _db.Orders.Where(c => c.User_ID == userId).Include(c => c.ApplicationUser).Include(c => c.OrderDetail).ToList();

            return View(findOrder);
        }
    }
}