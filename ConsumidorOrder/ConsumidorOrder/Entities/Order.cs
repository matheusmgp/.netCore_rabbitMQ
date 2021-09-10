using System;
using System.Collections.Generic;
using System.Text;

namespace ConsumidorOrder.Entities
{
    class Order
    {
        public int OrderNumber { get; set; }

        public string ItemName { get; set; }

        public double Price { get; set; }
    }
}
