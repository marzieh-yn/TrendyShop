using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TrendyShops.Model
{
    public class ProCatSub
    {
        [Key]
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public virtual Product Product { get; set; }
        [Required]
        public int? Sub_CategoryId { get; set; }
        [ForeignKey("Sub_CategoryId")]
        public virtual Sub_Category? Sub_Category { get; set; }

        public bool Status { get; set; }
    }
}
