using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrendyShops.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TrendyShops.DataAccess.Data;
using TrendyShops.Model;

namespace TrendyShops.Pages.Admin
{
    [Authorize(Roles = "Manager")]
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IList<TrendyShops.Model.ShoppingCart> ShoppingCart { get; set; }
        public List<OrderDetails> OrderDetail { get; private set; }
        public OrderDetails OrderDetails { get; private set; }

        public IEnumerable<ProductColors> ProductColors { get; private set; }
        public IEnumerable<ProductSizes> ProductSizes { get; private set; }
        public async Task OnGetAsync()
        {
            OrderDetail = _unitOfWork.OrderDetail.GetAll(includeProperties: "OrderHeader,Product,ProductColors,ProductSizes").ToList();
            ProductColors = _unitOfWork.ProductColors.GetAll();
            ProductSizes = _unitOfWork.ProductSizes.GetAll();
        }
    }
}
