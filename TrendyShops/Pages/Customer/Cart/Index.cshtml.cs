using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrendyShops.DataAccess.DbInitializer;
using TrendyShops.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using TrendyShops.Model;
using TrendyShops.DataAccess.Data;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using TrendyShops.Utility;

namespace TrendyShops.Pages.Customer.Cart
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public double CartTotal { get; set; }
        public double FinalPrice { get; set; }
        public double CurrentPrices { get; set; }

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CartTotal = 0;
            FinalPrice = 0;
            CurrentPrices = 0;
        }

        [BindProperty]
        public IList<TrendyShops.Model.ShoppingCart> ShoppingCart { get; set; }
        public IEnumerable<TrendyShops.Model.ProTypes> ProTypes { get; set; }
        public IEnumerable<ShoppingCart> ShoppingCarts { get; private set; }
        public IEnumerable<TrendyShops.Model.Category> Category { get; set; }
        public IEnumerable<TrendyShops.Model.ProCatSub> ProCatSub { get; set; }
        public IEnumerable<TrendyShops.Model.Sub_Category> Sub_Category { get; set; }
        public void OnGet()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var UserId = claim?.Value;
            Sub_Category = _unitOfWork.Sub_Category.GetAll(includeProperties: "Category");
            ProCatSub = _unitOfWork.ProCatSub.GetAll(includeProperties: "Product,Sub_Category");

            Category = _unitOfWork.Category.GetAll();
            ProTypes = _unitOfWork.ProTypes.GetAll(includeProperties: "Product");
            if (claim != null)
            {
                ShoppingCart = _unitOfWork.ShoppingCart.GetAll(filter: u => u.ApplicationUserId == UserId, includeProperties: "Product,ProductColors,ProductSizes").ToList();
                foreach (var cartItem in ShoppingCart)
                {
                    //double price = cartItem.Product.CurrnetPrice;
                    CartTotal += (cartItem.Product.CurrnetPrice * cartItem.Count);
                    FinalPrice = CartTotal + 5;
                    CurrentPrices = (cartItem.Product.CurrnetPrice * cartItem.Count);
                }
            }
            if (UserId == null)
            {
                ViewData["Count"] = 0;
            }

            if (UserId != null)
            {
                ShoppingCarts = _unitOfWork.ShoppingCart.GetAll(filter: u => u.ApplicationUserId == UserId, includeProperties: "Product");
                ViewData["Count"] = ShoppingCarts.Count();
            }



        }
        public IActionResult OnPostPlus(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
            _unitOfWork.ShoppingCart.IncrementCount(cart, 1);
            return RedirectToPage("/Customer/Cart/Index");
        }
        public IActionResult OnPostMinus(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
            if (cart.Count == 1)
            {
                var count = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count - 1;

                _unitOfWork.ShoppingCart.Remove(cart);
                _unitOfWork.Save();
                HttpContext.Session.SetInt32(SD.SessionCart, count);
            }
            else
            {
                _unitOfWork.ShoppingCart.DecrementCount(cart, 1);
            }
            return RedirectToPage("/Customer/Cart/Index");
        }
        public IActionResult OnPostRemove(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);


            var count = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count - 1;

            _unitOfWork.ShoppingCart.Remove(cart);
            _unitOfWork.Save();
            HttpContext.Session.SetInt32(SD.SessionCart, count);
            return RedirectToPage("/Customer/Cart/Index");
        }
    }
}
