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

namespace TrendyShops.Pages.Admin.ProCatSub
{
    [Authorize(Roles = "Manager")]
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public IEnumerable<TrendyShops.Model.ProCatSub> ProCatSub { get;set; }
        public IEnumerable<TrendyShops.Model.Sub_Category> Sub_Category { get; set; }

        public async Task OnGetAsync()
        {
            ProCatSub =  _unitOfWork.ProCatSub.GetAll(includeProperties: "Product,Sub_Category");
            Sub_Category = _unitOfWork.Sub_Category.GetAll(includeProperties: "Category");
        }
    }
}
