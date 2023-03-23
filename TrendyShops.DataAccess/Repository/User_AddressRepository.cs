using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrendyShops.DataAccess.Data;
using TrendyShops.DataAccess.Repository.IRepository;
using TrendyShops.Model;
using Microsoft.EntityFrameworkCore;

namespace TrendyShops.DataAccess.Repository
{
    public class User_AddressRepository : Repository<User_Address>, IUser_AddressRepository
    {
        private readonly ApplicationDbContext _db;

        public User_AddressRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(User_Address obj)
        {
            _db.Set<User_Address>().Attach(obj);
            _db.Entry(obj).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
