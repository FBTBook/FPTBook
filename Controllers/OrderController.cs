using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FPTBook.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _db;

        public OrderController(ILogger<OrderController> db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}