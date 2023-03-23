using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrendyShops.Model
{
	public class OrderDetails
	{
		public int Id { get; set; }
		[Required]
		public int? OrderId { get; set; }
		[ForeignKey("OrderId")]
		public OrderHeader? OrderHeader { get; set; }

		[Required]
		public int? ProductId { get; set; }

		public virtual Product? Product { get; set; }
		[Required]
		public int? ProductSizeId { get; set; }

		public virtual ProductSizes? ProductSizes { get; set; }
		[Required]
		public int? ProductColorId { get; set; }

		public virtual ProductColors? ProductColors { get; set; }

		public int Count { get; set; }
		[Required]
		public double? ProductPrice { get; set; }
		public string? ProductName { get; set; }
	}
}
