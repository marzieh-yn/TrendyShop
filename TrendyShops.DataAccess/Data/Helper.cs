using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Identity.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TrendyShops.DataAccess.Data;
using TrendyShops.Model;
using TrendyShops.DataAccess.Repository.IRepository;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TrendyShops.DataAccess.Data
{
    public class Helper
    {
        private readonly IUnitOfWork _unitOfWork;

        public Helper(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }
        [BindProperty]
        public IEnumerable<ShoppingCart> ShoppingCarts { get; set; }

        public string RemoveFromCart(string first, string last)
        {
            return $"{first} {last}";
        }
        public string ShoppingCart(IdentityUser User)
        {
            //var claimsIdentity = User;
            //var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var UserId = User.Id;

            ShoppingCarts = _unitOfWork.ShoppingCart.GetAll(filter: u => u.ApplicationUserId == UserId, includeProperties: "Product");

            if (ShoppingCarts == null)
            {
                var Count = 0;
                return $"{Count}";
            

            }else
            {

                var Count  = ShoppingCarts.Count();
                return $"{Count}";

            }
        }

        public IActionResult ShoppingCart(string count)
        {
            throw new NotImplementedException();
        }
    }
}
