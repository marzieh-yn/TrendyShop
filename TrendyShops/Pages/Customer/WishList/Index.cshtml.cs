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
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Wish_List> Wish_List { get;set; }

        public async Task OnGetAsync()
        {
            Wish_List = await _context.Wish_List
                .Include(w => w.UserId)
                .Include(w => w.Product).ToListAsync();
        }
    }
}
