using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrendyShops.DataAccess.Data;
using TrendyShops.DataAccess.Repository.IRepository;
using TrendyShops.Model;
namespace TrendyShop.Pages
{
    
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ILogger<IndexModel> _logger;
        public double CartTotal { get; set; }

        //private readonly ApplicationDbContext _context;



        
        public IndexModel(ILogger<IndexModel> logger, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            CartTotal = 0;
        }

        public IEnumerable<TrendyShops.Model.Category> Category { get; set; }
        public IEnumerable<TrendyShops.Model.Product> Product { get; set; }
        public IEnumerable<TrendyShops.Model.ProCatSub> ProCatSub { get; set; }
        public IEnumerable<TrendyShops.Model.Sub_Category> Sub_Category { get; set; }
        public IEnumerable<TrendyShops.Model.ProTypes> ProTypes { get; set; }

        [BindProperty]
        public IEnumerable<ShoppingCart> ShoppingCarts { get; set; }
        public ShoppingCart ShoppingCart { get; private set; }

        public async Task OnGetAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var UserId = claim?.Value;
            ProTypes = _unitOfWork.ProTypes.GetAll(includeProperties: "Product");
            Sub_Category = _unitOfWork.Sub_Category.GetAll(includeProperties: "Category");
            ProCatSub = _unitOfWork.ProCatSub.GetAll(includeProperties: "Product,Sub_Category");

            Category = _unitOfWork.Category.GetAll();
            Product = _unitOfWork.Product.GetAll();
            ShoppingCarts =  _unitOfWork.ShoppingCart.GetAll(filter: u => u.ApplicationUserId == UserId , includeProperties: "Product");
            
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