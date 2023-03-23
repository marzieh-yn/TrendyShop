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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Product obj)
        {
            var objFromDb = _db.Product.FirstOrDefault(u => u.Id == obj.Id);
            objFromDb.Name = obj.Name;
            objFromDb.Short_description = obj.Short_description;
            objFromDb.Long_description = obj.Long_description;
            objFromDb.LastPrice = obj.LastPrice;
            objFromDb.CurrnetPrice = obj.CurrnetPrice;
            objFromDb.ViewCounter = obj.ViewCounter; 
            objFromDb.Availability = obj.Availability; 
            objFromDb.Status = obj.Status;
            if (objFromDb.Image != null)
            {
                objFromDb.Image = obj.Image;
            }

        }
        
    }
}
