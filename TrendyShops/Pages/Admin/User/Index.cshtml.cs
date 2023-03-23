using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrendyShops.DataAccess.Data;
using TrendyShops.Model;
namespace TrendyShops.Pages.Admin.User
{
    [Authorize(Roles = "Manager")]
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        [BindProperty]
        public IList<UserRolesViewModel> model { get; set; } = new List<UserRolesViewModel>();
        public class UserRolesViewModel
        {
            public string Id { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
        }
        public IndexModel(
            UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task OnGetAsync()
        {
            List<IdentityUser> identityUsers = _userManager.Users.ToList();
            var userss = identityUsers;
            var users = await _userManager.Users.ToListAsync();

            foreach (IdentityUser user in users)
            {
                UserRolesViewModel urv = new UserRolesViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                };

                model.Add(urv);
            }
        }
    }
}
