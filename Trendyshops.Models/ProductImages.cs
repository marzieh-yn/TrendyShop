using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrendyShops.Model
{
    public class ProductImages
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        [Required]
        public string? Image { get; set; }
        public bool Status { get; set; }

        public DateTime Created_Date { get; set; }

    }
}
