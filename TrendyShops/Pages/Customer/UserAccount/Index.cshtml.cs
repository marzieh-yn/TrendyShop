using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrendyShops.DataAccess.Data;
using TrendyShops.DataAccess.Repository.IRepository;
using TrendyShops.Model;


namespace TrendyShops.Pages.Customer.UserAccount
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public IEnumerable<ShoppingCart> ShoppingCarts { get; set; }
        public IEnumerable<ProCatSub> ProCatSub { get; set; }
        public IEnumerable<TrendyShops.Model.Category> Category { get; set; }
  
        public IEnumerable<TrendyShops.Model.Sub_Category> Sub_Category { get; set; }
        public async Task OnGetAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var UserId = claim?.Value;
            ShoppingCarts = _unitOfWork.ShoppingCart.GetAll(filter: u => u.ApplicationUserId == UserId, includeProperties: "Product");
         
            Sub_Category = _unitOfWork.Sub_Category.GetAll(includeProperties: "Category");
            ProCatSub = _unitOfWork.ProCatSub.GetAll(includeProperties: "Product,Sub_Category");

            Category = _unitOfWork.Category.GetAll();
           
            if (ShoppingCarts == null)
            {

                ViewData["Count"] = 0;
            }

            if (ShoppingCarts != null)
            {

                ViewData["Count"] = ShoppingCarts.Count();
            }
        }
    }
}
