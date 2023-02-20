using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FPTBook.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _db;

        public CategoryController(ILogger<CategoryController> db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }   
    }
}