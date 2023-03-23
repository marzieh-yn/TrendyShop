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

namespace TrendyShops.Pages.Admin.Products
{
    [Authorize(Roles = "Manager")]

    public class EditModel : PageModel
    {
        
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public EditModel(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Product.FirstOrDefaultAsync(m => m.Id == id);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            string webRootPath = _hostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Product).State = EntityState.Modified;

            try
            {
                if (files.Count > 0)
                {

                    string fileName_new = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"Images/Products");
                    var extension = Path.GetExtension(files[0].FileName);
                    //var oldImagePath = Path.Combine(webRootPath, Product.Image.TrimStart('/'));
                    //if (System.IO.File.Exists(oldImagePath))
                    //{
                    //    System.IO.File.Delete(oldImagePath);
                    //}
                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName_new + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    Product.Image = @"/Images/Products/" + fileName_new + extension;
                }
                else if (files.Count == 0)
                {
                    _context.Entry(Product).Property(x => x.Image).IsModified = false;
                }
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
    //[Authorize]
    //public class EditModel : PageModel
    //{
    //    private readonly IUnitOfWork _unitOfWork;
    //    private readonly IWebHostEnvironment _hostEnvironment;
    //    public EditModel( IWebHostEnvironment hostEnvironment, IUnitOfWork unitOfWork)
    //    {
    //        _unitOfWork = unitOfWork;
    //        _hostEnvironment = hostEnvironment;
    //    }

    //    [BindProperty]
    //    public Product Product { get; set; }

    //    public async Task<IActionResult> OnGetAsync(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return NotFound();
    //        }

    //        Product = _unitOfWork.Product.GetFirstOrDefault(filter: u => u.Id == id);

    //        if (Product == null)
    //        {
    //            return NotFound();
    //        }
    //        return Page();
    //    }

    //    public async Task<IActionResult> OnPostAsync()
    //    {
    //        string webRootPath = _hostEnvironment.WebRootPath;
    //        var files = HttpContext.Request.Form.Files;

    //        if (!ModelState.IsValid)
    //        {
    //            return Page();
    //        }

    //        _unitOfWork.Product.Update(Product);


    //            if (files.Count > 0)
    //            {

    //                string fileName_new = Guid.NewGuid().ToString();
    //                var uploads = Path.Combine(webRootPath, @"Images/Products");
    //                var extension = Path.GetExtension(files[0].FileName);
    //                //var oldImagePath = Path.Combine(webRootPath, Product.Image.TrimStart('/'));
    //                //if (System.IO.File.Exists(oldImagePath))
    //                //{
    //                //    System.IO.File.Delete(oldImagePath);
    //                //}
    //                using (var fileStream = new FileStream(Path.Combine(uploads, fileName_new + extension), FileMode.Create))
    //                {
    //                    files[0].CopyTo(fileStream);
    //                }
    //                Product.Image = @"/Images/Products/" + fileName_new + extension;
    //            }
    //            else if (files.Count == 0)
    //            {

    //                _unitOfWork.Product.Update(Product);


    //                TempData["success"] = "Category Edited Successfuly!";
    //            }
    //            _unitOfWork.Save();



    //        return RedirectToPage("./Index");
    //    }


    //}
}
