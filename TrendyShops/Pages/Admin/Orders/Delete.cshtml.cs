using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public TrendyShops.Model.ShoppingCart ShoppingCart { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ShoppingCart = _unitOfWork.ShoppingCart.GetFirstOrDefault(filter: u => u.Id == id, includeProperties: "Product,ProductColors,ProductSizes");
               

            if (ShoppingCart == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ShoppingCart = _unitOfWork.ShoppingCart.GetFirstOrDefault(filter: u => u.Id == id);

            if (ShoppingCart != null)
            {
                _unitOfWork.ShoppingCart.Remove(ShoppingCart);
                _unitOfWork.Save();
            }

            return RedirectToPage("./Index");
        }
    }
}
