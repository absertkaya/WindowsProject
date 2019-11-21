using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightApp.Model
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }

        public IList<OrderLine> OrderLines { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public Passenger Customer { get; set; }

        public string TotalPriceString { get { return String.Format("€ {0:0.00}", OrderLines.Sum(o => o.Product.Price * o.Amount)); } }

        public Order(Passenger passenger)
        {
            OrderLines = new List<OrderLine>();
            OrderStatus = OrderStatus.PENDING;
            Customer = passenger;
        }

        public void AddProduct(Product product)
        {
            OrderLine orderLine = OrderLines.FirstOrDefault(o => o.Product.Equals(product));
            if (orderLine is null) OrderLines.Add(new OrderLine(product));
            else orderLine.Amount++;
        }

        public void DecrementProduct(Product product)
        {
            OrderLine orderLine = OrderLines.FirstOrDefault(o => o.Product.Equals(product));
            if (orderLine != null)
            {
                orderLine.Amount--;
                if (orderLine.Amount < 1) RemoveProduct(product);
            }

        }

        public void RemoveProduct(Product product)
        {
            OrderLines.Remove(OrderLines.FirstOrDefault(o => o.Product.Equals(product)));
        }
    }
}
