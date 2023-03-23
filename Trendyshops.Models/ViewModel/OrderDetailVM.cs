using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrendyShops.Model.ViewModel
{
    public class OrderDetailVM
    {
        public List<OrderHeader> OrderHeaders { get; set; }
        public OrderHeader OrderHeader { get; set; }

        public List<OrderDetails> OrderDetails { get; set; }
        public IEnumerable<User_Address> User_Address { get; set; }
    }
}
