using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrendyShops.DataAccess.Data;
using TrendyShops.DataAccess.Repository.IRepository;
using TrendyShops.Model;

namespace TrendyShops.Pages.Admin.SubCategory
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
        public Model.Sub_Category Sub_Category { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Sub_Category = _unitOfWork.Sub_Category.GetFirstOrDefault(filter: u => u.Id == id, includeProperties: "Category");

            if (Sub_Category == null)
            {
                return NotFound();
            }
           ViewData["CategoryId"] = new SelectList(_unitOfWork.Category.GetAll(), "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                return Page();
            }

            _unitOfWork.Sub_Category.Update(Sub_Category);

            _unitOfWork.Save();
            TempData["success"] = "Category Edited Successfuly!";


            return RedirectToPage("./Index");
        }

       
    }
}
