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

        [Authorize]
        public IActionResult AddToCart(int id)
        {
            if(ModelState.IsValid){
                var findBook = _db.Books.Find(id);
                if(findBook.Book_Quantity >0){
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var findCart = _db.Carts.Where(c => c.User_ID == userId).ToArray();
                    var findCartDetail = _db.CartDetails.Where(cd => cd.Cart_ID == findCart[0].Cart_ID && cd.Book_ID == id).ToArray();

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
                }
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult DeleteCart(int id)
        {
            if(ModelState.IsValid){
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var findCart = _db.Carts.Where(c => c.User_ID == userId).ToArray();
            var findCartDetail = _db.CartDetails.Where(cd => cd.Cart_ID == findCart[0].Cart_ID).ToArray();

            _db.CartDetails.Remove(findCartDetail[0]);
            _db.SaveChanges();                
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Updatecart(int idCartDetail, int idBook, int quantity)
        {
            if(ModelState.IsValid){
                var checkQuantity = _db.Books.Find(idBook);
                if(quantity <= checkQuantity.Book_Quantity){
                    var findCartDetail = _db.CartDetails.Find(idCartDetail);
                    findCartDetail.Cart_Quantity = quantity;
                    _db.CartDetails.Update(findCartDetail);
                    _db.SaveChanges();
                }
                else{
                    TempData["errorUpdateCart"] = "Quantity that you want to update more than current quantity";
                }
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult OrderCart(int idCart)
        {
            if(ModelState.IsValid){

            var findCartDetail = _db.CartDetails.Where(cd => cd.Cart_ID == idCart).Include(c => c.Book).ToArray();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 

            foreach (var od in findCartDetail)
            {
                var findBook = _db.Books.Find(od.Book_ID);
                if(od.Cart_Quantity > findBook.Book_Quantity){
                    TempData["errorOrder"] = "Quantity of " + findBook.Book_Name + " just quantity is " 
                    + findBook.Book_Quantity + ". So you can not order. Please update quantity again to Order";
                    return RedirectToAction("Index");
                }
            }

            Order order = new Order();
            order.Order_OrderDate = DateTime.Now;
            order.Order_DeliveryDate = null;
            order.Order_Status = 0;
            order.User_ID = userId;
            _db.Orders.Add(order);
            _db.SaveChanges();
            
            foreach (var od in findCartDetail)
            {
                var findBook = _db.Books.Find(od.Book_ID);
                if(od.Cart_Quantity > findBook.Book_Quantity){
                    return RedirectToAction("Index");
                }
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
            return RedirectToAction("Index");
        }
    }
}