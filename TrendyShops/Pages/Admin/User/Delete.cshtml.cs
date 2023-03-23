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
using Microsoft.AspNetCore.Identity;

namespace TrendyShops.Pages.Admin.User
{
    [Authorize(Roles = "Manager")]
    public class DeleteModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        [BindProperty]
        public IList<UserRolesViewModel> model { get; set; } = new List<UserRolesViewModel>();
        public class UserRolesViewModel
        {
            public string Id { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }

        }
        public DeleteModel(
            UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task OnGetAsync(string? id)
        {
            List<IdentityUser> identityUsers = _userManager.Users.ToList();
            var userss = identityUsers;
            var Id =  id;
            var users = await _userManager.FindByIdAsync(Id);

            //foreach (IdentityUser user in users)
            //{
                UserRolesViewModel urv = new UserRolesViewModel()
                {
                    Id = users.Id,
                    UserName = users.UserName,
                    Email = users.Email,
                    PhoneNumber = users.PhoneNumber
                };

                model.Add(urv);
            //}
        }
        
        public async Task<IActionResult> OnPostAsync(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Id = id;
            var users = await _userManager.FindByIdAsync(Id);

            if (users != null)
            {
                _userManager.DeleteAsync(users);
                //_userManager.SaveChanges();
                TempData["success"] = "Message Successfuly Deleted!";

            }
            TempData["success"] = "Message Successfuly Deleted!";

            return RedirectToPage("./Index");
        }
    }
}
