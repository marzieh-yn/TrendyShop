using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrendyShops.Model;

namespace TrendyShops.Model
{
    public class User_Address
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public string PostalCode { get; set; }
        public string ExtraDetailes { get; set; }

        public bool Status { get; set; }

        public DateTime Created_Date { get; set; }

    }
}
