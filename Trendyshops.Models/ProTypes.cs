using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TrendyShops.Model
{
    public class ProTypes
    {
        [Key]
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public virtual Product Product { get; set; }
        public bool NewArrival { get; set; }
        public bool ThreeItems { get; set; }
        public bool BestSeller { get; set; }

        public bool Status { get; set; }
    }
}
