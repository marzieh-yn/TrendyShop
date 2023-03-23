using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrendyShops.DataAccess.Data;
using TrendyShops.Model;
using TrendyShops.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using TrendyShops.Model.ViewModel;
using TrendyShops.Utility;
using Stripe;

namespace TrendyShops.Pages.Admin.Orders
{
    [Authorize(Roles = "Manager")]

    public class DetailsOrderModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        [BindProperty]
        public IList<UserRolesViewModel> model { get; set; } = new List<UserRolesViewModel>();
        public OrderDetailVM OrderDetailVM { get; set; }
        public class UserRolesViewModel
        {
            public string Id { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
        }
        public DetailsOrderModel(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public IList<TrendyShops.Model.ShoppingCart> ShoppingCart { get; set; }
        public List<OrderDetails> OrderDetail { get; private set; }
        public OrderHeader OrderHeader { get; set; }
        public IEnumerable<User_Address> User_Address { get; set; }

        public IEnumerable<ProductColors> ProductColors { get; private set; }
        public IEnumerable<ProductSizes> ProductSizes { get; private set; }

        public async Task OnGetAsync(int id)
        {

            OrderDetailVM = new()
            {
                OrderDetails = _unitOfWork.OrderDetail.GetAll(filter: m => m.OrderId == id, includeProperties: "OrderHeader,Product,ProductColors,ProductSizes").ToList(),
            OrderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(filter: m => m.Id == id, includeProperties: "User_Address"),
            User_Address = _unitOfWork.User_Address.GetAll() 
        };
        //ProductColors = _unitOfWork.ProductColors.GetAll();
        //ProductSizes = _unitOfWork.ProductSizes.GetAll();

            List<IdentityUser> identityUsers = _userManager.Users.ToList();
            var users = identityUsers;

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
        public IActionResult OnPostOrderCompleted(int orderId)
        {
            _unitOfWork.OrderHeader.UpdateStatus(orderId, SD.StatusCompleted);
            _unitOfWork.Save();
            return RedirectToPage("Index");
        }
        public IActionResult OnPostOrderRefund(int orderId)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(o => o.Id == orderId);

            var options = new RefundCreateOptions
            {
                Reason = RefundReasons.RequestedByCustomer,
                PaymentIntent = orderHeader.PaymentIntentId
            };

            var service = new RefundService();
            Refund refund = service.Create(options);

            _unitOfWork.OrderHeader.UpdateStatus(orderId, SD.StatusRefunded);
            _unitOfWork.Save();
            return RedirectToPage("Index");
        }

        public IActionResult OnPostOrderCancel(int orderId)
        {
            _unitOfWork.OrderHeader.UpdateStatus(orderId, SD.StatusCancelled);
            _unitOfWork.Save();
            return RedirectToPage("Index");
        }
    }
}