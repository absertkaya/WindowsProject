using System;

namespace FlightApp.Model
{
    public class OrderLine
    {
        public Order Order { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }

        public string SubPriceString { get { return String.Format("€ {0:0.00}", Product.Price * Amount); } }

        public OrderLine(Product product)
        {
            Product = product;
            Amount = 1;
        }
    }
}