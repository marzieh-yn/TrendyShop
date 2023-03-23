using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrendyShops.DataAccess.Data;
using TrendyShops.DataAccess.Repository.IRepository;
using TrendyShops.DataAccess.Repository;
using TrendyShops.Model;

namespace TrendyShops.DataAccess.Repository
{
     public class UnitOfWork : IUnitOfWork
    {  
            private readonly ApplicationDbContext _db;
            public UnitOfWork(ApplicationDbContext db)
            {
                _db = db;
                Category = new CategoryRepository(_db);
                ContactUs = new ContactUsRepository(_db);
                ShoppingCart = new ShoppingCartRepository(_db);
                OrderDetail = new OrderDetailRepository(_db);
                OrderHeader = new OrderHeaderRepository(_db);
                Product = new ProductRepository(_db);
                ProductImages = new ProductImagesRepository(_db);
                ProductColors = new ProductColorsRepository(_db);
                ProductSizes = new ProductSizesRepository(_db);
                ProTypes = new ProTypesRepository(_db);
                ProductReviews = new ProductReviewsRepository(_db);
                Sub_Category = new Sub_CategoryRepository(_db);
                User_Address = new User_AddressRepository(_db);
                Wish_List = new Wish_ListRepository(_db);
                ProCatSub = new ProCatSubRepository(_db);
                ApplicationUser = new ApplicationUserRepository(_db);

           
        }
        public ICategoryRepository Category {  get; private set; }
            public IContactUsRepository ContactUs { get; private set; }
            public IShoppingCartRepository ShoppingCart { get; private set; }
            public IOrderHeaderRepository OrderHeader { get; private set; }
            public IOrderDetailRepository OrderDetail { get; private set; }

      

        public IProductRepository Product { get; private set; }
            public IProductImagesRepository ProductImages { get; private set; }
            public IProductColorsRepository ProductColors { get; private set; }
            public IProductSizesRepository ProductSizes { get; private set; }
            public IProductReviewsRepository ProductReviews { get; private set; }
            public ISub_CategoryRepository Sub_Category { get; private set; }
            public IUser_AddressRepository User_Address { get; private set; }
            public IWish_ListRepository Wish_List { get; private set; }
            //public IApplicationUserRepository GetApplicationUser();
            public IProCatSubRepository ProCatSub { get; }
            public IApplicationUserRepository ApplicationUser { get; private set; }
        public IProTypesRepository ProTypes { get; }

        IEnumerable IUnitOfWork.ApplicationUser => throw new NotImplementedException();

        public void Dispose()
                {
                    _db.Dispose();
                }

        public IApplicationUserRepository GetApplicationUser()
        {
            throw new NotImplementedException();
        }

        public void Save()
            {
                _db.SaveChanges();
            }
        }
}
