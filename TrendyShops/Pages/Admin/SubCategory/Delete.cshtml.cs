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

namespace TrendyShops.Pages.Admin.SubCategory
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
        public Model.Sub_Category Sub_Category { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Sub_Category = _unitOfWork.Sub_Category
                .GetFirstOrDefault(filter: m => m.Id == id, includeProperties: "Category");

            if (Sub_Category == null)
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
            Sub_Category = _unitOfWork.Sub_Category.GetFirstOrDefault(filter: u => u.Id == id);

            if (Sub_Category != null)
            {
                _unitOfWork.Sub_Category.Remove(Sub_Category);
                _unitOfWork.Save();
            }
           

            return RedirectToPage("./Index");
        }
    }
}
