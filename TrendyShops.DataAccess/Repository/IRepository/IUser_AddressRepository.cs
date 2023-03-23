using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrendyShops.Model;

namespace TrendyShops.DataAccess.Repository.IRepository
{
    public interface IUser_AddressRepository : IRepository<User_Address>
    {
        void Update(User_Address obj);
    }
}
