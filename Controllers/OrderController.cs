using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LoginFPTBook.Data;
using LoginFPTBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FPTBook.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _db;

        public OrderController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var orders = _db.Orders.Include(o => o.ApplicationUser).ToList();
            return View(orders);
        }

        public IActionResult orderDetail(int idOrder)
        {
            var orderDetails = _db.OrderDetails.Where(o => o.Order_ID == idOrder).Include(o => o.Order.ApplicationUser).Include(o => o.Book);
            return View(orderDetails);
            // ViewData["OrderDetail"] = _db.OrderDetails.Where(o => o.Order_ID == idOrder).Include(o => o.Order.ApplicationUser);
            // return View();
        }

        [HttpPost]
        public IActionResult EditOrderdetail(Order obj, string idUser, int status)
        {
            if(ModelState.IsValid){  
                obj.User_ID = idUser;
                obj.Order_Status = status;
                _db.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("orderDetail", new {idOrder = obj.Order_ID});
        }
    }
}