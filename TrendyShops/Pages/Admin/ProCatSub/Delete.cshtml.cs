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

namespace TrendyShops.Pages.Admin.ProCatSub
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
        public TrendyShops.Model.ProCatSub ProCatSub { get; set; }

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
            return Page();
        }
        [BindProperty]
        public TrendyShops.Model.ProCatSub ProCatSubs { get; set; }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProCatSubs =  _unitOfWork.ProCatSub.GetFirstOrDefault(m => m.Id == id);

            if (ProCatSubs != null)
            {
                _unitOfWork.ProCatSub.Remove(ProCatSubs);
                 _unitOfWork.Save();
            }

            return RedirectToPage("./Index");
        }
    }
}
