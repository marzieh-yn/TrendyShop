using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrendyShops.Model;

namespace TrendyShops.DataAccess.Repository.IRepository
{
    public interface ISub_CategoryRepository : IRepository<Sub_Category>
    {
        void Update(Sub_Category obj);
    }
}
