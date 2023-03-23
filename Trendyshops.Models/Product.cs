using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrendyShops.Model

{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Short_description { get; set; }
        public string? Long_description { get; set; }
        public double? LastPrice { get; set; }
        public double CurrnetPrice { get; set; }
        public int ViewCounter { get; set; }
       
        public string? Image { get; set; }
        public bool Availability { get; set; }
        public bool Status { get; set; }
        public DateTime Created_Date { get; set; }








    }
}
