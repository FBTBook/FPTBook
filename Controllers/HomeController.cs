using LoginFPTBook.Data;
using LoginFPTBook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
        public IActionResult Privacy()
        {
            return View();
        }

    }
}