using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrendyShops.DataAccess.Data;
using TrendyShops.DataAccess.Repository.IRepository;
using TrendyShops.Model;

namespace TrendyShops.Pages.Admin.Category
{
    [Authorize(Roles = "Manager")]
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public IEnumerable<TrendyShops.Model.Category> Category { get;set; }

        public async Task OnGetAsync()
        {
            Category =  _unitOfWork.Category.GetAll();
        }
    }
}
