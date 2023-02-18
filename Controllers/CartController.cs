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

            // var findCartDetail = (from cd in findCart select cd.Cart_ID).ToString();

            // var cartId = Convert.ToInt32(findCartDetail);

            var cartInfor = _db.CartDetails.Where(c => c.Cart_ID == findCart[0].Cart_ID).Include(c => c.Book).ToList();
            return View(cartInfor);
        }
        // public IActionResult AddToCart(int bookId)
        // {
        //     var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
        //     // var check = from c in Carts where c.User_ID == userId and c.Book_ID == bookId select c;

        //     var cart = _db.Carts.Where(c => c.User_ID == userId && c.Book_ID == bookId).ToList();
            
        //     // if(check == null){
        //         // Cart cart = new Cart();
        //         // cart.Cart_Quantity = 1;
        //         // var a = _db.Users.Find(userId);
        //         // var b = _db.Books.Find(bookId);
                
        //         // cart.User_ID = userId;
        //         // cart.Book_ID = bookId;
        //         // cart.ApplicationUser = _db.Users.Find(userId);
        //         // cart.Book = _db.Books.Find(bookId);
        //         if (cart.Count()<=0)
        //         {
        //             _db.Carts.Add(new Cart(){Cart_Quantity = 1, User_ID = userId, Book_ID = 2});
        //             _db.SaveChanges();
        //             return RedirectToAction("Index");
        //         }
        //         else
        //         {
        //             cart[0].Cart_Quantity += 1;
        //             _db.SaveChanges();
        //             return RedirectToAction("Index");
        //         }
                

        //     // }else{
        //     //     return NoContent();
        //         // check.Cart_Quantity += 1;
        //     // }
        //     _db.SaveChanges();
        //     return RedirectToAction("Index");
        // }
        // public IActionResult Delete(int id)
        // {
        //    Cart cart = _db.Carts.Find(id);
        //     _db.Carts.Remove(cart);
        //     _db.SaveChanges();
        //     return RedirectToAction("Index");
        // }
    }
}