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

namespace LoginFPTBook.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var findCart = _db.Carts.Where(c => c.User_ID == userId).ToArray();
            var cartInfor = _db.CartDetails.Where(c => c.Cart_ID == findCart[0].Cart_ID).Include(c => c.Book).ToList();

            decimal totalPrie = 0;
            foreach (var p in cartInfor)
            {
                totalPrie += (p.Cart_Quantity*p.Book.Book_SalePrice);
            }
            ViewData["data"] = totalPrie.ToString();

            return View(cartInfor);
        }

        public IActionResult AddToCart(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var findCart = _db.Carts.Where(c => c.User_ID == userId).ToArray();
            var findCartDetail = _db.CartDetails.Where(cd => cd.Cart_ID == findCart[0].Cart_ID).ToArray();

            if(findCartDetail.Count() != 0){
                findCartDetail[0].Cart_Quantity += 1;
                _db.CartDetails.Update(findCartDetail[0]);
            }
            else{

                CartDetail cartDetail = new CartDetail();

                cartDetail.Cart_Quantity = 1;
                cartDetail.Cart_ID = findCart[0].Cart_ID;
                cartDetail.Book_ID = id;
                _db.CartDetails.Add(cartDetail);
            }  
            _db.SaveChanges();
            
            return RedirectToAction("Index");
        }

        public IActionResult DeleteCart(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var findCart = _db.Carts.Where(c => c.User_ID == userId).ToArray();
            var findCartDetail = _db.CartDetails.Where(cd => cd.Cart_ID == findCart[0].Cart_ID).ToArray();

            _db.CartDetails.Remove(findCartDetail[0]);
            _db.SaveChanges();
            
            return RedirectToAction("Index");
        }

        public IActionResult Updatecart(int idCartDetail, CartDetail obj, int idBook, int idCart, int quantity)
        {
            obj.CartDetail_ID = idCartDetail;
            obj.Book_ID = idBook;
            obj.Cart_ID = idCart;
            obj.Cart_Quantity = quantity;
            _db.CartDetails.Update(obj);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // public IActionResult OrderCart(List<CartDetail> lstCartDetail)
        // {
        //     var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //     Order order = new Order();
        //     order.Order_OrderDate = DateTime.Now;
        //     order.Order_DeliveryDate = null;
        //     order.Order_Status = 0;
        //     order.User_ID = userId;
        //     _db.Orders.Add(order);
        //     _db.SaveChanges();
        //     foreach (var od in lstCartDetail)
        //     {
        //         OrderDetail orderDetail = new OrderDetail();
        //         orderDetail.OrderDetail_Quantity = od.Cart_Quantity;
        //         orderDetail.OrderDetail_Price = od.Book.Book_Price;
        //         orderDetail.Order_ID = order.Order_ID;
        //         orderDetail.Book_ID = od.Book_ID;
        //         _db.OrderDetails.Add(orderDetail);
        //     }
        //     _db.SaveChanges();
        //     return RedirectToAction("Index", "Home");
        // }

        public IActionResult OrderCart(int idCart)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            Order order = new Order();
            order.Order_OrderDate = DateTime.Now;
            order.Order_DeliveryDate = null;
            order.Order_Status = 0;
            order.User_ID = userId;
            _db.Orders.Add(order);
            _db.SaveChanges();
            var findCartDetail = _db.CartDetails.Where(cd => cd.Cart_ID == idCart).Include(c => c.Book).ToArray();
            foreach (var od in findCartDetail)
            {
                OrderDetail orderDetail = new OrderDetail();
                orderDetail.OrderDetail_Quantity = od.Cart_Quantity;
                orderDetail.OrderDetail_Price = od.Book.Book_SalePrice;
                orderDetail.Order_ID = order.Order_ID;
                orderDetail.Book_ID = od.Book_ID;
                _db.OrderDetails.Add(orderDetail);
            }
            foreach (var od in findCartDetail)
            {
                _db.CartDetails.Remove(od);
            }
            _db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}