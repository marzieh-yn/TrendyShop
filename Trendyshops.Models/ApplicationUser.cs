using Microsoft.AspNetCore.Identity;

namespace TrendyShops.Model
{
    public class ApplicationUser : IdentityUser
    {
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
    }
}
