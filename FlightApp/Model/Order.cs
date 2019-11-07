using System;
using System.Collections.Generic;

namespace FlightApp.Model
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }

        public IList<OrderLine> OrderLines { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public Passenger Customer { get; set; }
    }
}
