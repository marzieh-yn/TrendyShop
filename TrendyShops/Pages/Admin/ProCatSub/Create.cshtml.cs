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

namespace TrendyShops.Pages.Admin.ProCatSub
{
    [Authorize(Roles = "Manager")]
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        [BindProperty]
        public IEnumerable<TrendyShops.Model.Sub_Category> Sub_Category { get; set; }

        //public IList<TrendyShops.Model.Sub_Category> Sub_Category { get; set; }
        public IList<TrendyShops.Model.Category> Category { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["CategoryId"] = new SelectList(_unitOfWork.Category.GetAll(), "Id", "Name");
            ViewData["ProductId"] = new SelectList(_unitOfWork.Product.GetAll(), "Id", "Name");
            ViewData["Sub_CategoryId"] = new SelectList(_unitOfWork.Sub_Category.GetAll(), "Id", "Name");
            Sub_Category = _unitOfWork.Sub_Category.GetAll(includeProperties: "Category");
               

            return Page();
        }

        [BindProperty]
        public TrendyShops.Model.ProCatSub ProCatSub { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                return Page();
            }

            _unitOfWork.ProCatSub.Add(ProCatSub);
             _unitOfWork.Save();

            return RedirectToPage("./Index");
        }
    }
}
