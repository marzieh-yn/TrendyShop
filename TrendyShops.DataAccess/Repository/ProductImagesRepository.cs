using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrendyShops.DataAccess.Repository.IRepository;
using TrendyShops.Model;
using TrendyShops.DataAccess.Data;


namespace TrendyShops.DataAccess.Repository
{
    public class ProductImagesRepository : Repository<ProductImages>, IProductImagesRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductImagesRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(ProductImages obj)
        {
            var objFromDb = _context.ProductImages.FirstOrDefault(u => u.Id == obj.Id);
            
            objFromDb.Status = obj.Status;
            if (objFromDb.Image != null)
            {
                objFromDb.Image = obj.Image;
            }
        }
    }
}
