using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrendyShops.Model;

namespace TrendyShops.DataAccess.Repository.IRepository
{
    public interface IWish_ListRepository : IRepository<Wish_List>
    {
        void Update(Wish_List obj);
    }
}
