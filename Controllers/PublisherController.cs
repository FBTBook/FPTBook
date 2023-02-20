using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FPTBook.Controllers
{
    // [Route("[controller]")]
    public class PublisherController : Controller
    {
        private readonly ILogger<PublisherController> _db;

        public PublisherController(ILogger<PublisherController> db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult addPublisher()
        {
            // TODO: Your code here
            return View();
        }

        public IActionResult updatePublisher()
        {
            // TODO: Your code here
            return View();
        }
        
        
    }
}