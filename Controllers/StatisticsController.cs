using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FPTBook.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly ILogger<StatisticsController> _db;

        public StatisticsController(ILogger<StatisticsController> db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}