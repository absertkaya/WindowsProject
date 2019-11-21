using FlightApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightApp.Data.DTOs
{

    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public IList<OrderLineDTO> OrderLines { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public PassengerDTO Customer { get; set; }

        public Order ToOrder()
        {
            return new Order(Customer.ToPassenger())
            {
                Id = this.Id,
                TimeStamp = this.TimeStamp,
                OrderLines = this.OrderLines.Select(o => o.ToOrderLine()).ToList(),
                OrderStatus = this.OrderStatus
            };
        }
    }
    public class OrderLineDTO
    {
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }

        public static OrderLineDTO FromOrderLine(OrderLine orderLine)
        {
            return new OrderLineDTO
            {
                ProductId = orderLine.Product.Id,
                Amount = orderLine.Amount
            };
        }
        public OrderLine ToOrderLine()
        {
            return new OrderLine(Product)
            {
                Amount = this.Amount
            };
        }
    }
}
