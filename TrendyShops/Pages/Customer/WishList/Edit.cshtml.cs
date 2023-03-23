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

namespace TrendyShops.Pages.Customer.WishList
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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
           ViewData["UserId"] = new SelectList(_context.ApplicationUser, "Id", "Id");
           ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Wish_List).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Wish_ListExists(Wish_List.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool Wish_ListExists(int id)
        {
            return _context.Wish_List.Any(e => e.Id == id);
        }
    }
}
