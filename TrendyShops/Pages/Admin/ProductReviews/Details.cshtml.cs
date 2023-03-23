using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrendyShops.DataAccess.Data;
using TrendyShops.Model;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using TrendyShops.DataAccess.Repository.IRepository;

namespace TrendyShops.Pages.Admin.ProductReviews
{
    [Authorize(Roles = "Manager")]
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public DetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public TrendyShops.Model.ProductReviews ProductReviews { get; set; }
        public TrendyShops.Model.ApplicationUser ApplicationUser { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductReviews = _unitOfWork.ProductReviews
                .GetFirstOrDefault(filter: u => u.Id == id, includeProperties: "Product");


            if (ProductReviews == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
