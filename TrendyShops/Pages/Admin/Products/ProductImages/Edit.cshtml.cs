using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrendyShops.DataAccess.Data;
using TrendyShops.Model;
using TrendyShops.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;

namespace TrendyShops.Pages.Admin.Products.ProductImages
{
    [Authorize(Roles = "Manager")]
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public EditModel(IUnitOfWork unitOfWork,IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;

        }

        [BindProperty]
        public TrendyShops.Model.ProductImages ProductImages { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductImages = _unitOfWork.ProductImages
                .GetFirstOrDefault(filter: u => u.Id == id, includeProperties: "Product");

            if (ProductImages == null)
            {
                return NotFound();
            }
           ViewData["ProductId"] = new SelectList(_unitOfWork.Product.GetAll(), "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            string webRootPath = _hostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            //edit
            var objFromDb = _unitOfWork.ProductImages.GetFirstOrDefault(u => u.Id == ProductImages.Id);
            if (files.Count > 0)
            {
                string fileName_new = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"images\menuItems");
                var extension = Path.GetExtension(files[0].FileName);

                //delete the old image
                var oldImagePath = Path.Combine(webRootPath, objFromDb.Image.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
                //new upload
                using (var fileStream = new FileStream(Path.Combine(uploads, fileName_new + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                ProductImages.Image = @"\images\menuItems\" + fileName_new + extension;
            }
            else
            {
                ProductImages.Image = objFromDb.Image;
            }
            _unitOfWork.ProductImages.Update(ProductImages);
            _unitOfWork.Save();

            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            //_unitOfWork.ProductImages.Update(ProductImages);

            //if (files.Count > 0)
            //{

            //    string fileName_new = Guid.NewGuid().ToString();
            //    var uploads = Path.Combine(webRootPath, @"Images/Products");
            //    var extension = Path.GetExtension(files[0].FileName);
            //    //var oldImagePath = Path.Combine(webRootPath, Product.Image.TrimStart('/'));
            //    //if (System.IO.File.Exists(oldImagePath))
            //    //{
            //    //    System.IO.File.Delete(oldImagePath);
            //    //}
            //    using (var fileStream = new FileStream(Path.Combine(uploads, fileName_new + extension), FileMode.Create))
            //    {
            //        files[0].CopyTo(fileStream);
            //    }
            //    ProductImages.Image = @"/Images/Products/" + fileName_new + extension;
            //}
            //else if (files.Count == 0)
            //{

            //    _unitOfWork.ProductImages.Update(ProductImages);


            //    TempData["success"] = "Category Edited Successfuly!";
            //}
            //_unitOfWork.Save();

            return RedirectToPage("./Index");
        }

    }
}
