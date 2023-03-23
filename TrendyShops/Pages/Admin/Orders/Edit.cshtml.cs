using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public OrderDetails OrderDetail { get; private set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrderDetail = _unitOfWork.OrderDetail.GetFirstOrDefault(filter: u => u.Id == id, includeProperties: "Product,ProductColors,ProductSizes");
                

            if (OrderDetail == null)
            {
                return NotFound();
            }
           //ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id");
           ViewData["ProductId"] = new SelectList(_unitOfWork.Product.GetAll(), "Id", "Name");
           ViewData["ProductColorsId"] = new SelectList(_unitOfWork.ProductColors.GetAll(), "Id", "Color");
           ViewData["ProductSizesId"] = new SelectList(_unitOfWork.ProductSizes.GetAll(), "Id", "Size");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _unitOfWork.OrderDetail.Update(OrderDetail);

            _unitOfWork.Save();
            TempData["success"] = "Category Edited Successfuly!";


            return RedirectToPage("./Index");
        }

       
    }
}
