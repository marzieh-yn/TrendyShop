using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrendyShops.Model
{
	public class ShoppingCart
	{
        public ShoppingCart()
        {
			Count = 1;
        }
		public int Id { get; set; }
		public int ProductId { get; set; }
		[ForeignKey("ProductId")]
		[ValidateNever]
		public Product Product { get; set; }

		public int? ProductSizesId { get; set; }
		public ProductSizes ProductSizes { get; set; }

		public int? ProductColorsId { get; set; }
		public ProductColors ProductColors { get; set; }

		//public int? ProductSizeId { get; set; }
		//public ProductSizes ProductSizes { get; set; }

		//public int? ProductColorId { get; set; }
		//public ProductColors ProductColors { get; set; }

		[Range(1, 100, ErrorMessage = "Please select a count between 1 and 100")]
		public int Count { get; set; }
		public string ApplicationUserId { get; set; }
		[ForeignKey("ApplicationUserId")]
		[ValidateNever]
		public ApplicationUser ApplicationUser { get; set; }
		public DateTime Created_Date { get; set; }
		
	}
}
