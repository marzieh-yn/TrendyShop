using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrendyShops.DataAccess.Data;
using TrendyShops.DataAccess.Repository.IRepository;
using TrendyShops.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace TrendyShops.Pages.Customer
{
    public class SearchResultModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public int NotAvailability { get; set; }
        public int Availability { get; set; }
        public SearchResultModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            NotAvailability = 0;
            Availability = 0;

        }

        [BindProperty]
        public IEnumerable<ShoppingCart> ShoppingCarts { get; set; }
        public IEnumerable<Sub_Category> Sub_Category { get; private set; }
        public IEnumerable<ProCatSub> ProCatSub { get; private set; }
        public IEnumerable<Category> Category { get; private set; }
        public IEnumerable<Product> Products { get; private set; }
        public IEnumerable<TrendyShops.Model.ProTypes> ProTypes { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        public async Task OnGet(int id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var UserId = claim?.Value;
            ShoppingCarts = _unitOfWork.ShoppingCart.GetAll(filter: u => u.ApplicationUserId == UserId, includeProperties: "Product");
            Sub_Category = _unitOfWork.Sub_Category.GetAll(includeProperties: "Category");
            ProCatSub = _unitOfWork.ProCatSub.GetAll(includeProperties: "Product,Sub_Category");

            Category = _unitOfWork.Category.GetAll();
        
            ProTypes = _unitOfWork.ProTypes.GetAll(includeProperties: "Product");

            var products = _unitOfWork.Product.GetAll();

            if (!string.IsNullOrEmpty(SearchString))
            {
                products = products.Where(s => s.Name.Contains(SearchString));
            }

            Products = products.ToList();
            NotAvailability = products.Where( u => u.Availability == false).Count();
            Availability = products.Where(u => u.Availability == true).Count();

           
            if (ShoppingCarts == null)
            {

                ViewData["Count"] = 0;
            }

            if (ShoppingCarts != null)
            {

                ViewData["Count"] = ShoppingCarts.Count();
            }
        }
        public IActionResult OnPostNotAvailability(int cartId)
        {
            var products = _unitOfWork.Product.GetAll();

            if (!string.IsNullOrEmpty(SearchString))
            {
                products = products.Where(s => s.Name.Contains(SearchString));
            }

            Products = products.ToList();
            NotAvailability = products.Where(u => u.Availability == false).Count();
            Availability = products.Where(u => u.Availability == true).Count();
            return RedirectToPage("/Customer/SearchResult");
        }
    }
}
