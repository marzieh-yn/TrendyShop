using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrendyShops.DataAccess.Repository.IRepository;
using TrendyShops.Model;
using TrendyShops.DataAccess.Data;
using Microsoft.EntityFrameworkCore;


namespace TrendyShops.DataAccess.Repository
{
    public class ProTypesRepository : Repository<ProTypes>, IProTypesRepository
    {
        private readonly ApplicationDbContext _db;

        public ProTypesRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(ProTypes obj)
        {
            _db.Set<ProTypes>().Attach(obj);
            _db.Entry(obj).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
