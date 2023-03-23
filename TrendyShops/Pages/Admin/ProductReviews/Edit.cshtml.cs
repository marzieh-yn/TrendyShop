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
using Microsoft.EntityFrameworkCore;
using TrendyShops.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;

namespace TrendyShops.Pages.Admin.ProductReviews
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
        public TrendyShops.Model.ProductReviews ProductReviews { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductReviews = _unitOfWork.ProductReviews
                .GetFirstOrDefault(filter: u => u.Id == id, includeProperties: "Product");

            if (ProductReviews == null)
            {
                return NotFound();
            }
           //ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                return Page();
            }
            _unitOfWork.ProductReviews.Update(ProductReviews);

            _unitOfWork.Save();
            TempData["success"] = "Category Edited Successfuly!";


            return RedirectToPage("./Index");
        }

       
    }
}
