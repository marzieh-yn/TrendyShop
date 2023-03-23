using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrendyShops.Model;
using Microsoft.EntityFrameworkCore.Design;

namespace TrendyShops.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Category { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }
        public DbSet<ProductColors> ProductColors { get; set; }
        public DbSet<ProductSizes> ProductSizes { get; set; }
        public DbSet<ProductReviews> ProductReviews { get; set; }
        public DbSet<Sub_Category> Sub_Category { get; set; }
        public DbSet<User_Address> User_Address { get; set; }
        public DbSet<Wish_List> Wish_List { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<ProCatSub> ProCatSub { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<OrderHeader> OrderHeader { get; set; }
        public DbSet<ProTypes> ProTypes { get; set; }
    }
}
