using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LoginFPTBook.Constants;
using LoginFPTBook.Data;
using LoginFPTBook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FPTBook.Controllers
{
    public class PublisherController : Controller
    {
        private readonly ApplicationDbContext _db;
        public PublisherController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Authorize(Roles="Admin")]
        public IActionResult Index()
        {
            IEnumerable<Publisher> publishers = _db.Publishers.ToList();
            return View(publishers);
        }

        [Authorize(Roles="Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles="Admin")]
        public IActionResult Create(Publisher obj)
        {
            if(ModelState.IsValid){
                _db.Publishers.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        [Authorize(Roles="Admin")]
        public IActionResult Edit(int id)
        {
            Publisher publisher = _db.Publishers.Find(id);
            if (publisher == null)
            {
                return RedirectToAction("Index");
            }
            return View(publisher);
        }

        [HttpPost]
        [Authorize(Roles="Admin")]
        public IActionResult Edit(int id, Publisher obj)
        {
            if (ModelState.IsValid)
            {
                obj.Publisher_ID = id;
                _db.Publishers.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}