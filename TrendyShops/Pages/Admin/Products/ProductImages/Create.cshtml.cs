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

namespace TrendyShops.Pages.Admin.Products.ProductImages
{
    [Authorize(Roles = "Manager")]
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public CreateModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        
        {
                _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        [BindProperty]
        public Product Product { get; set; }
        public IActionResult OnGet()
        {
        ViewData["ProductId"] = new SelectList(_unitOfWork.Product.GetAll(), "Id", "Name");
            return Page();
        }

        [BindProperty]
        public TrendyShops.Model.ProductImages ProductImages { get; set; }

        //To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            string webRootPath = _hostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            //if (files.Count > 0)
            //{
            //    foreach (var item in files)
            //    {
                    string fileName_new = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"Images/Products");
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(uploads, fileName_new + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                ProductImages.Image = @"/Images/Products/" + fileName_new + extension;
            //    }
            //}
            if (ModelState.IsValid)
                {
                    return Page();
                }

            _unitOfWork.ProductImages.Add(ProductImages);
            _unitOfWork.Save();
                TempData["success"] = "Product Created Successfuly!";
            
            return RedirectToPage("./Index");
        }


    }
}
