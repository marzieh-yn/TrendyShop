using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrendyShops.Model;

namespace TrendyShops.DataAccess.Repository.IRepository
{
    public interface IProTypesRepository : IRepository<ProTypes>
    {
        void Update(ProTypes obj);
    }
}
