using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrendyShops.DataAccess.Data;
using TrendyShops.Model;
using TrendyShops.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;

namespace TrendyShops.Pages.Admin.Products
{
    [Authorize(Roles = "Manager")]
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public CreateModel( IWebHostEnvironment hostEnvironment, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;       
            _hostEnvironment = hostEnvironment;
        }
        [BindProperty]
        public Product Product { get; set; }
  
        public IActionResult OnGet()
        {
            return Page();
        }

        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            string webRootPath = _hostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            string fileName_new = Guid.NewGuid().ToString();
            var uploads = Path.Combine(webRootPath, @"Images/Products");
            var extension = Path.GetExtension(files[0].FileName);

            using (var fileStream = new FileStream(Path.Combine(uploads, fileName_new + extension), FileMode.Create))
            {
                files[0].CopyTo(fileStream);
            }
            Product.Image = @"/Images/Products/" + fileName_new + extension;

                if (!ModelState.IsValid)
            {
                return Page();
            }

           _unitOfWork.Product.Add(Product);
            _unitOfWork.Save();
            TempData["success"] = "Product Created Successfuly!";
            return RedirectToPage("./Index");
        }
    }
}
//string fileName_new = Guid.NewGuid().ToString();
//var uploads = Path.Combine(webRootPath, @"Images/Products");
//var extension = Path.GetExtension(files[0].FileName);


//var oldImagePath = Path.Combine(webRootPath, item.Image.TrimStart('/'));
//if (System.IO.File.Exists(oldImagePath))
//{
//    System.IO.File.Delete(oldImagePath);
//}


//using (var fileStream = new FileStream(Path.Combine(uploads, fileName_new + extension), FileMode.Create))
//{
//    files[0].CopyTo(fileStream);
//}
//Product.Image = @"/Images/Products/" + fileName_new + extension;
