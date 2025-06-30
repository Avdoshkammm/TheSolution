using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolution.Domain.Entities
{
    public class Order
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public User User { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
