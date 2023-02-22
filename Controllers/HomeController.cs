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
            IEnumerable<Book> books = _db.Books.Where(b => b.Book_Status == 1).ToList();
            return View(books);
        }
        public IActionResult Search(string search)
        {
            IEnumerable<Book> books = _db.Books.Where(b => b.Book_Status == 1 && (b.Book_Name.Contains(search) || b.Book_Name.EndsWith(search))).ToList();
            return View(books);
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