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

namespace TrendyShops.Pages.Customer
{
    public class ProductListModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductListModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public IEnumerable<ShoppingCart> ShoppingCarts { get; set; }
        public IEnumerable<ProCatSub> ProCatSub { get; set; }
        public IEnumerable<ProCatSub> ProCatSubs { get; set; }

        public IEnumerable<TrendyShops.Model.Category> Category { get; set; }
        public IEnumerable<ProTypes> ProTypes { get; set; }
        public IEnumerable<Sub_Category> Sub_Category { get; private set; }

        public async Task OnGetAsync(int id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var UserId = claim?.Value;
            ShoppingCarts = _unitOfWork.ShoppingCart.GetAll(filter: u => u.ApplicationUserId == UserId, includeProperties: "Product");
            ProCatSubs  = _unitOfWork.ProCatSub.GetAll(filter: u => u.Sub_CategoryId == id, includeProperties: "Product");
            //ProCatSub = _unitOfWork.ProCatSub.GetAll(includeProperties: "Product,Sub_Category");
            ProTypes = _unitOfWork.ProTypes.GetAll(includeProperties: "Product");
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
