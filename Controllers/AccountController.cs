using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FPTBook.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _db;

        public AccountController(ILogger<AccountController> db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult updateAccount()
        {
            return View();
        }
        
        public IActionResult createOwnerAccount()
        {
            return View();
        }
        
    }
}