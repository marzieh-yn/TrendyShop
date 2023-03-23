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

namespace TrendyShops.Pages.Admin.ContactUs
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
        public TrendyShops.Model.ContactUs ContactUs { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ContactUs =  _unitOfWork.ContactUs.GetFirstOrDefault(m => m.Id == id);

            if (ContactUs == null)
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

            ContactUs =  _unitOfWork.ContactUs.GetFirstOrDefault(m => m.Id == id);

            if (ContactUs != null)
            {
                _unitOfWork.ContactUs.Remove(ContactUs);
                 _unitOfWork.Save();
                TempData["success"] = "Message Successfuly Deleted!";

            }
            TempData["success"] = "Message Successfuly Deleted!";

            return RedirectToPage("./Index");
        }
    }
}
