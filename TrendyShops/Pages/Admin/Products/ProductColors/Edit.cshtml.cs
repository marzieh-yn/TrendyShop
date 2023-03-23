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

namespace TrendyShops.Pages.Admin.Products.ProductColors
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
        public TrendyShops.Model.ProductColors ProductColors { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductColors = _unitOfWork.ProductColors.GetFirstOrDefault(filter: u => u.Id == id, includeProperties: "Product");

            if (ProductColors == null)
            {
                return NotFound();
            }
           ViewData["ProductId"] = new SelectList(_unitOfWork.Product.GetAll(), "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                return Page();
            }

            _unitOfWork.ProductColors.Update(ProductColors);

            _unitOfWork.Save();
            TempData["success"] = "Category Edited Successfuly!";

            return RedirectToPage("./Index");
        }

        
    }
}
