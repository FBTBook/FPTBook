using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FPTBook.Controllers
{
    public class BookController : Controller
    {
        private readonly ILogger<BookController> _db;

        public BookController(ILogger<BookController> db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult addBook()
        {
            return View();
        }       

        public IActionResult updateBook()
        {
            return View();
        }
         
    }
}