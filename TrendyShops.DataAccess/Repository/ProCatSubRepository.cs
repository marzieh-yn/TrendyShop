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
    public class ProCatSubRepository : Repository<ProCatSub>, IProCatSubRepository
    {
        private readonly ApplicationDbContext _db;

        public ProCatSubRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(ProCatSub obj)
        {
            _db.Set<ProCatSub>().Attach(obj);
            _db.Entry(obj).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
