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
    public class Wish_ListRepository : Repository<Wish_List>, IWish_ListRepository
    {
        private readonly ApplicationDbContext _db;

        public Wish_ListRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Wish_List obj)
        {
            _db.Set<Wish_List>().Attach(obj);
            _db.Entry(obj).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
