using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LoginFPTBook.Data;
using LoginFPTBook.Models;
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
        public IActionResult Index()
        {
            IEnumerable<Publisher> publishers = _db.Publishers.ToList();
            return View(publishers);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Publisher obj)
        {
            if(ModelState.IsValid){
                _db.Publishers.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
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
        public IActionResult Delete(int id)
        {
            Publisher publisher = _db.Publishers.Find(id);
            if(publisher != null){
                publisher.Publisher_Status = 0;
                _db.Publishers.Update(publisher);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult DeleteForever(int id)
        {
            Publisher publisher = _db.Publishers.Find(id);
            _db.Publishers.Remove(publisher);
            _db.SaveChanges();
            return RedirectToAction("Index");
        } 
    }
}