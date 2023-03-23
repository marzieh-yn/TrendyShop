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
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public EditModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        [BindProperty]
        public TrendyShops.Model.ProCatSub ProCatSub { get; set; }
        public IEnumerable<TrendyShops.Model.Sub_Category> Sub_Category { get; set; }
        public IEnumerable<TrendyShops.Model.Category> Category { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }           
            ProCatSub = _unitOfWork.ProCatSub.GetFirstOrDefault(filter: u => u.Id == id, includeProperties: "Product,Sub_Category");
                
            if (ProCatSub == null)
            {
                return NotFound();
            }
           ViewData["CategoryId"] = new SelectList(_unitOfWork.Category.GetAll(), "Id", "Name");
           ViewData["ProductId"] = new SelectList(_unitOfWork.Product.GetAll(), "Id", "Name");
           ViewData["Sub_CategoryId"] = new SelectList(_unitOfWork.Sub_Category.GetAll(), "Id", "Name");
            Sub_Category =  _unitOfWork.Sub_Category.GetAll(includeProperties : "Category");
               
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                return Page();
            }
                _unitOfWork.ProCatSub.Update(ProCatSub);
            
                 _unitOfWork.Save();
                TempData["success"] = "Category Edited Successfuly!";

            return RedirectToPage("./Index");
        }
       
    }
}
