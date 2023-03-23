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

namespace TrendyShops.Pages.Admin.Products.ProductSizes
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
        public TrendyShops.Model.ProductSizes ProductSizes { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductSizes = _unitOfWork.ProductSizes
                .GetFirstOrDefault(filter: u => u.Id == id, includeProperties: "Product");

            if (ProductSizes == null)
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

            _unitOfWork.ProductSizes.Update(ProductSizes);

            _unitOfWork.Save();
            TempData["success"] = "Category Edited Successfuly!";

            return RedirectToPage("./Index");
        }

     
    }
}
