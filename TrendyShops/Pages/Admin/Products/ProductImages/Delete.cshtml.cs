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

namespace TrendyShops.Pages.Admin.Products.ProductImages
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
        public TrendyShops.Model.ProductImages ProductImages { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductImages = _unitOfWork.ProductImages
                .GetFirstOrDefault(filter:m => m.Id == id , includeProperties: "Product");

            if (ProductImages == null)
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

            ProductImages = _unitOfWork.ProductImages.GetFirstOrDefault(filter: m => m.Id == id);

            if (ProductImages != null)
            {
                _unitOfWork.ProductImages.Remove(ProductImages);
                _unitOfWork.Save();
            }

            return RedirectToPage("./Index");
        }
    }
}
