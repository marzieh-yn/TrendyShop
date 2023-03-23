using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace TrendyShops.Model
{
    public class ContactUs
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string Phone_number { get; set; }
        public bool Status { get; set; }
        [DataMember]
        public DateTime Created_Date { get; set; }




    }
}
