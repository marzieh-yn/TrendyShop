using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrendyShops.DataAccess.Data;
using TrendyShops.Model;
using TrendyShops.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;

namespace TrendyShops.Pages.Admin.SubCategory
{
    [Authorize(Roles = "Manager")]
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult OnGet()
        {
        ViewData["CategoryId"] = new SelectList(_unitOfWork.Category.GetAll(), "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Model.Sub_Category Sub_Category { get; set; }

        //To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                return Page();
            }

            _unitOfWork.Sub_Category.Add(Sub_Category);
            _unitOfWork.Save();
            TempData["success"] = "Sub Category Created Successfuly!";
            return RedirectToPage("./Index");
        }
    }
}
