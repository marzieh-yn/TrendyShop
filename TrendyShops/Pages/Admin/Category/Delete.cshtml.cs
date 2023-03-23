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

namespace TrendyShops.Pages.Admin.Category
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        [BindProperty]
        public TrendyShops.Model.Category Category { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category =  _unitOfWork.Category.GetFirstOrDefault(m => m.Id == id);

            if (Category == null)
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

            Category =  _unitOfWork.Category.GetFirstOrDefault(m => m.Id == id);

            if (Category != null)
            {
                _unitOfWork.Category.Remove(Category);
                 _unitOfWork.Save();
                TempData["success"] = "Category Deleted Successfuly!";

            }

            return RedirectToPage("./Index");
        }
    }
}
