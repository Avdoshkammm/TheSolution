﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolution.Domain.Entities
{
    public class OrderProduct
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public Order Order { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
        public decimal TotalAmount { get; set; } //итоговая стоимость заказа (т.к. цена - decimal)

    }
}
