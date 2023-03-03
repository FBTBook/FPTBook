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
            if(ModelState.IsValid){
            IEnumerable<Book> books = _db.Books.Where(b => b.Book_Status == 1 && (b.Book_Name==search || b.Book_Name.Contains(search) || b.Book_Name.StartsWith(search) || b.Book_Name.EndsWith(search))).ToList();
            return View(books);                
            }
            return RedirectToAction("Index");
        }
        [Authorize]
        public IActionResult OrderHistory()
        {
            if(ModelState.IsValid){
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var findOrder = _db.Orders.Where(c => c.User_ID == userId).Include(c => c.ApplicationUser).Include(c => c.OrderDetail).ToList();
            return View(findOrder);                
            }
            return RedirectToAction("Index");
        }
        [Authorize]
        public IActionResult CancelOrder(int idOrder)
        {
            if(ModelState.IsValid){
                var findOrder = _db.Orders.Find(idOrder);
                findOrder.Order_Status = 1;
                _db.Orders.Update(findOrder);
                _db.SaveChanges();
            }
            return RedirectToAction("OrderHistory");
        }
        public IActionResult Help()
        {
            return View();
        }
    }
}