using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LoginFPTBook.Data;
using LoginFPTBook.Models;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles="Admin, Owner")]
        public IActionResult Index()
        {
            var orders = _db.Orders.Include(o => o.ApplicationUser).ToList();
            return View(orders);
        }

        [Authorize(Roles="Admin, Owner")]
        public IActionResult orderDetail(int idOrder)
        {
            if(ModelState.IsValid){
            var orderDetails = _db.OrderDetails.Where(o => o.Order_ID == idOrder).Include(o => o.Order.ApplicationUser).Include(o => o.Book);
            return View(orderDetails);                
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles="Admin, Owner")]
        public IActionResult EditOrderdetail(Order obj, string idUser, int status, DateTime orderDate)
        {
            if(ModelState.IsValid){  
                if(status == 4){
                    obj.Order_Status = 2;
                    var findOrderDetail = _db.OrderDetails.Where(od => od.Order_ID == obj.Order_ID).ToArray();
                    foreach (var od in findOrderDetail)
                    {
                        var findBook = _db.Books.Find(od.Book_ID);
                        if(od.OrderDetail_Quantity > findBook.Book_Quantity){
                            return RedirectToAction("orderDetail", new {idOrder = obj.Order_ID});
                        }
                    }
                    foreach (var od in findOrderDetail)
                    {
                        od.Book.Book_Quantity = od.Book.Book_Quantity - od.OrderDetail_Quantity;
                        _db.Update(od.Book);
                    }
                }
                else{
                    obj.Order_Status = status;   
                    if(status == 3){
                    obj.Order_DeliveryDate = DateTime.Now;
                    }
                }
                obj.User_ID = idUser;
                obj.Order_OrderDate = orderDate;
                _db.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("orderDetail", new {idOrder = obj.Order_ID});
        }
    }
}