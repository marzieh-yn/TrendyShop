using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrendyShops.DataAccess.Data;
using TrendyShops.Model;

namespace TrendyShops.Pages.Customer.WishList
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Wish_List Wish_List { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Wish_List = await _context.Wish_List
                .Include(w => w.UserId)
                .Include(w => w.Product).FirstOrDefaultAsync(m => m.Id == id);

            if (Wish_List == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
