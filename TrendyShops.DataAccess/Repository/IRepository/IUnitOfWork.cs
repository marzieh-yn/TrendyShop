using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrendyShops.Model;
using TrendyShops.DataAccess.Data;
using System.Collections;

namespace TrendyShops.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Category { get; }
        IContactUsRepository ContactUs { get; }
        IProductRepository Product { get;  }
        IProductImagesRepository ProductImages { get;}
        IProductColorsRepository ProductColors { get;}
        IProductSizesRepository ProductSizes { get; }
        IProTypesRepository ProTypes { get; }
        IProductReviewsRepository ProductReviews { get;  }
        ISub_CategoryRepository Sub_Category { get; }
        IUser_AddressRepository User_Address { get; }
        IWish_ListRepository Wish_List { get; }
        IApplicationUserRepository GetApplicationUser();
        IProCatSubRepository ProCatSub { get;  }
        IShoppingCartRepository ShoppingCart { get;  }
        IOrderHeaderRepository OrderHeader { get; }
        IOrderDetailRepository OrderDetail { get; }
        IEnumerable ApplicationUser { get; }

        void Save();
    }
}
