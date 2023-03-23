using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrendyShops.DataAccess.Data;
using TrendyShops.Model;
using TrendyShops.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;

namespace TrendyShops.Pages.Admin.Orders
{
    [Authorize(Roles = "Manager")]
    public class DetailsModel : PageModel 
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        [BindProperty]
        public IList<UserRolesViewModel> model { get; set; } = new List<UserRolesViewModel>();

        public class UserRolesViewModel
        {
            public string UserName { get; set; }
            public string Email { get; set; }
        }
        public DetailsModel(IUnitOfWork unitOfWork,        
        UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;            
            _userManager = userManager;
        }
        public TrendyShops.Model.ShoppingCart ShoppingCart { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ShoppingCart = _unitOfWork.ShoppingCart.GetFirstOrDefault(filter: u => u.Id == id, includeProperties: "Product,ProductColors,ProductSizes");
                

            List<IdentityUser> identityUsers = _userManager.Users.ToList();
            var userss = identityUsers;
            var users = await _userManager.Users.Where(m => m.Id == ShoppingCart.ApplicationUserId).ToListAsync();

            foreach (IdentityUser user in users)
            {
                UserRolesViewModel urv = new UserRolesViewModel()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                };

                model.Add(urv);
            }
           

            if (ShoppingCart == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
