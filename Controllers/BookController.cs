using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LoginFPTBook.Data;
using LoginFPTBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FPTBook.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Book> books = _db.Books.Include(b => b.Category).Include(b => b.Publisher).ToList();
            return View(books);
        }
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _db.Books == null)
            {
                return NotFound();
            }
            var product = await _db.Books
                .Include(p => p.Category).Include(p => p.Publisher)
                .FirstOrDefaultAsync(m => m.Book_ID == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        public IActionResult Create()
        {
            ViewData["Category"] = _db.Categories.Where(c => c.Category_Status == 1).ToList();
            ViewData["Publisher"] = _db.Publishers.Where(p => p.Publisher_Status == 1).ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Book obj, IFormFile Book_Image)
        {
            ViewData["Category"] = _db.Categories.Where(c => c.Category_Status == 1).ToList();
            ViewData["Publisher"] = _db.Publishers.Where(p => p.Publisher_Status == 1).ToList();
            if (ModelState.IsValid)
            {
                var filePaths = new List<string>();
                if (Book_Image.Length > 0)
                {
                    string fileType = Path.GetExtension(Book_Image.FileName).ToLower().Trim();
                    if (fileType != ".jpg" && fileType != ".png")
                    {
                        TempData["msg"] = "File Format Not Supported. Only .jpg and .png !";
                        return View(obj);
                    }
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", Book_Image.FileName);
                    filePaths.Add(filePath);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Book_Image.CopyToAsync(stream);
                    }
                    obj.Book_Image = Book_Image.FileName;
                    _db.Books.Add(obj);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(obj);
        }
        public IActionResult Edit(int id)
        {
            Book book = _db.Books.Find(id);
            if (book == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["Category"] = _db.Categories.Where(c => c.Category_Status == 1).ToList();
                ViewData["Publisher"] = _db.Publishers.Where(p => p.Publisher_Status == 1).ToList();
                // ViewData["Category_ID"] = new SelectList(_db.Categories, "Category_ID", "Category_Name", book.Category_ID);
                // ViewData["Publisher_ID"] = new SelectList(_db.Publishers, "Publisher_ID", "Publisher_Name", book.Publisher_ID);
                return View(book);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Book obj, IFormFile? UpdateImg)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Category"] = _db.Categories.Where(c => c.Category_Status == 1).ToList();
                ViewData["Publisher"] = _db.Publishers.Where(p => p.Publisher_Status == 1).ToList();
                return RedirectToAction("Edit", new { id = obj.Book_ID });
            }
            else
            {
                if (UpdateImg != null)
                {
                    var filePaths = new List<string>();
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", UpdateImg.FileName);
                    filePaths.Add(filePath);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await UpdateImg.CopyToAsync(stream);
                    }
                    obj.Book_Image = UpdateImg.FileName;
                }
                _db.Books.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        public IActionResult Delete(int id)
        {
            Book obj = _db.Books.Find(id);
            if (obj != null)
            {
                obj.Book_Status = 0;
                _db.Books.Update(obj);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult DeleteForever(int id)
        {
            Book obj = _db.Books.Find(id);
            if (obj != null)
            {
                _db.Books.Remove(obj);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}