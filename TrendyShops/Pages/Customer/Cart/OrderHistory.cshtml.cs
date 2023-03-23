using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using TrendyShops.DataAccess.Repository.IRepository;
using TrendyShops.Model;
using TrendyShops.Model.ViewModel;

namespace TrendyShops.Pages.Customer.Cart
{
    public class OrderHistoryModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public IList<UserRolesViewModel> model { get; set; } = new List<UserRolesViewModel>();
        public OrderDetailVM OrderDetailVM { get; set; }
        public class UserRolesViewModel
        {
            public string Id { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
        }
        public OrderHistoryModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
         
        }
        public IEnumerable<ShoppingCart> ShoppingCarts { get; set; }
        public ShoppingCart ShoppingCart { get; private set; }
        public IEnumerable<TrendyShops.Model.Category> Category { get; set; }
        public IEnumerable<TrendyShops.Model.ProCatSub> ProCatSub { get; set; }
        public IEnumerable<TrendyShops.Model.Sub_Category> Sub_Category { get; set; }
        public void OnGet()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var UserId = claim?.Value;
            Sub_Category = _unitOfWork.Sub_Category.GetAll(includeProperties: "Category");
            ProCatSub = _unitOfWork.ProCatSub.GetAll(includeProperties: "Product,Sub_Category");

            Category = _unitOfWork.Category.GetAll();
            OrderDetailVM = new()
            {
                OrderDetails = _unitOfWork.OrderDetail.GetAll( includeProperties: "OrderHeader,Product,ProductColors,ProductSizes").ToList(),
                OrderHeaders = _unitOfWork.OrderHeader.GetAll(filter: u => u.ApplicationUserId == UserId, includeProperties: "User_Address").ToList(),
                User_Address = _unitOfWork.User_Address.GetAll()
            };

            ShoppingCarts = _unitOfWork.ShoppingCart.GetAll(filter: u => u.ApplicationUserId == UserId, includeProperties: "Product");


            if (UserId == null)
            {
                ViewData["Count"] = 0;
            }

            if (UserId != null)
            {
                ShoppingCarts = _unitOfWork.ShoppingCart.GetAll(filter: u => u.ApplicationUserId == UserId, includeProperties: "Product");
                ViewData["Count"] = ShoppingCarts.Count();
            }
        }
    }
}
