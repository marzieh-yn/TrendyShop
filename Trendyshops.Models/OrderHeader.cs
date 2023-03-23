using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrendyShops.Model
{
	public class OrderHeader
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string? ApplicationUserId { get; set; }
		[ForeignKey("ApplicationUserId")]
		public ApplicationUser? ApplicationUser { get; set; }

		[Required]
		[DisplayFormat(DataFormatString = "{0:C}")]
		[Display(Name = "Order Total")]

		public double OrderTotal { get; set; }

		[Required]
		[Display(Name = "Pick Up Time")]
		public DateTime PickUpTime { get; set; }

		[Required]
		[NotMapped]
		public DateTime PickUpDate { get; set; }

		public int? User_AddressId { get; set; }
		public virtual User_Address? User_Address { get; set; }
		//public int? ShoppingCartId { get; set; }
		
		//public virtual ShoppingCart? ShoppingCart { get; set; }
		public string? Status { get; set; }

		public string? Comments { get; set; }

		public string? SessionId { get; set; }
		public string? PaymentIntentId { get; set; }

		[Display(Name = "Pickup Name")]
		
		public string? PickupName { get; set; }
		
		[Display(Name = "Phone Number")]
		public string? Phone_Number { get; set; }
	
		public DateTime OrderDate { get; set; }
	}
}
