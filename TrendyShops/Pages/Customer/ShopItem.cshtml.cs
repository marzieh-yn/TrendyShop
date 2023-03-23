
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using TrendyShops.Model;

using TrendyShops.DataAccess.Data;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using TrendyShops.DataAccess.DbInitializer;
using TrendyShops.DataAccess.Repository.IRepository;
//using TrendyShops.Repository.IRepository;

namespace TrendyShops.Pages.Customer
{
    [Authorize]

    public class ShopItemModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShopItemModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public ShoppingCart ShoppingCart { get; set; }
        public Product Product { get; set; }
        public ProductColors ProductColors { get; set; }
        //public ProductReviews ProductReviews { get; set; }
        public ProductSizes ProductSizes { get; set; }
        public IList<ProductImages> ProductImages { get; set; }
        public IEnumerable<TrendyShops.Model.ProTypes> ProTypes { get; set; }

        public IEnumerable<TrendyShops.Model.Category> Category { get; set; }
        public IEnumerable<TrendyShops.Model.ProCatSub> ProCatSub { get; set; }
        public IEnumerable<TrendyShops.Model.Sub_Category> Sub_Category { get; set; }
        public IEnumerable<TrendyShops.Model.ProductReviews> ProductReviewss { get; set; }
        [BindProperty]
        public IEnumerable<ShoppingCart> ShoppingCarts { get; set; }



        public void OnGet(int id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var UserId = claim?.Value;
            ProTypes = _unitOfWork.ProTypes.GetAll(includeProperties: "Product");
            Sub_Category = _unitOfWork.Sub_Category.GetAll(includeProperties: "Category");
            ProCatSub = _unitOfWork.ProCatSub.GetAll(includeProperties: "Product,Sub_Category");

            Category = _unitOfWork.Category.GetAll();

            if (UserId == null)
            {               
                ViewData["Count"] = 0;
            }

            if (UserId != null)
            {              
                ShoppingCarts = _unitOfWork.ShoppingCart.GetAll(filter: u => u.ApplicationUserId == UserId, includeProperties: "Product"); 
                ViewData["Count"] = ShoppingCarts.Count();
            }



            Product = _unitOfWork.Product.GetFirstOrDefault(m => m.Id == id);
            ProductReviewss = _unitOfWork.ProductReviews.GetAll().ToList();

            List<ProductColors> resultColors = _unitOfWork.ProductColors.GetAll(m => m.ProductId == id).ToList();
            List<ProductSizes> resultSizes = _unitOfWork.ProductSizes.GetAll(m => m.ProductId == id).ToList();

            //ViewData["UserId"] = new SelectList(_unitOfWork.ApplicationUser, "Id", "ProductId", "Size");
            ViewData["ProductSizess"] = new SelectList(resultSizes, "Id", "Size");
            ViewData["ProductColors"] = new SelectList(resultColors, "Id", "Color");

            ProCatSub = _unitOfWork.ProCatSub.GetAll(includeProperties: "Product,Sub_Category");
                
            Category = _unitOfWork.Category.GetAll().ToList();
            ProductImages = _unitOfWork.ProductImages.GetAll().ToList();

            ProductReviews = new()
            {
                UserId = claim.Value,
                Product = _unitOfWork.Product.GetFirstOrDefault(m => m.Id == id),
                ProductId = id
            };
           

            ShoppingCart = new()
            {
                ApplicationUserId = claim.Value,
                Product = _unitOfWork.Product.GetFirstOrDefault(m => m.Id == id),
                ProductId = id
            };
        }
       
        public IActionResult OnPostAddToCart(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/index");
            
                ShoppingCart shoppingCartFromDb = _unitOfWork.ShoppingCart.GetFirstOrDefault(
                    u => u.ApplicationUserId == ShoppingCart.ApplicationUserId &&
                    u.ProductId == ShoppingCart.ProductId &&
                    u.ProductColorsId == ShoppingCart.ProductColorsId &&
                    u.ProductSizesId == ShoppingCart.ProductSizesId);

                if (shoppingCartFromDb == null)
                {

                _unitOfWork.ShoppingCart.Add(ShoppingCart);
                _unitOfWork.Save();
                     }
                else
                {
                var ent = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.ApplicationUserId == ShoppingCart.ApplicationUserId && u.ProductId == ShoppingCart.ProductId &&
                u.ProductColorsId == ShoppingCart.ProductColorsId &&
                u.ProductSizesId == ShoppingCart.ProductSizesId);
                     ent.Count = ent.Count+ ShoppingCart.Count;
                _unitOfWork.Save();
            }
                return LocalRedirect(returnUrl);
            
        }



        [BindProperty]
        public TrendyShops.Model.ProductReviews ProductReviews { get; set; }
        public LocalRedirectResult OnPostReview(int? id, string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/Customer/ShopItem?id="+id);
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var UserId = claim.Value;
            _unitOfWork.ProductReviews.Add(ProductReviews);
            _unitOfWork.Save();
            return LocalRedirect(returnUrl);

        }
    }
}
