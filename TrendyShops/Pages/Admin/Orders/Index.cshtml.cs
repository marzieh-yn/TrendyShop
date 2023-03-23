using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrendyShops.DataAccess.Data;
using TrendyShops.Model;
using TrendyShops.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;

namespace TrendyShops.Pages.Admin.Orders
{
    [Authorize(Roles = "Manager")]
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        [BindProperty]
        public IList<UserRolesViewModel> model { get; set; } = new List<UserRolesViewModel>();

        public class UserRolesViewModel
        {
            public string Id { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
        }
        public IndexModel(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public IList<TrendyShops.Model.ShoppingCart> ShoppingCart { get;set; }
        public List<OrderDetails> OrderDetail { get; private set; }
        public OrderDetails OrderDetails { get; private set; }

        public IEnumerable<ProductColors> ProductColors { get; private set; }
        public IEnumerable<ProductSizes> ProductSizes { get; private set; }

        public async Task OnGetAsync()
        {


            OrderDetail = _unitOfWork.OrderDetail.GetAll( includeProperties: "OrderHeader,Product,ProductColors,ProductSizes").ToList();
            ProductColors = _unitOfWork.ProductColors.GetAll();
            ProductSizes = _unitOfWork.ProductSizes.GetAll();
            List<IdentityUser> identityUsers = _userManager.Users.ToList();
            var users = identityUsers;
            //var users = await _userManager.Users.Where(m => m.Id == ShoppingCart.ApplicationUserId).ToListAsync();

            foreach (IdentityUser user in users)
            {
                UserRolesViewModel urv = new UserRolesViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                };

                model.Add(urv);
            }
        }
        public  IActionResult OnPostDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrderDetails = _unitOfWork.OrderDetail.GetFirstOrDefault(filter: u => u.Id == id);

           
                _unitOfWork.OrderDetail.Remove(OrderDetails);
                _unitOfWork.Save();
            

            return Page();
        }
    }
}
