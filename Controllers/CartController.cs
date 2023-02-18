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

namespace LoginFPTBook.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var findCart = _db.Carts.Where(c => c.User_ID == userId).ToArray();
            var cartInfor = _db.CartDetails.Where(c => c.Cart_ID == findCart[0].Cart_ID).Include(c => c.Book).ToList();

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
        
    }
}