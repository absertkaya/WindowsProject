using FlightApp.Model;
using FlightApp.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace FlightApp.Data
{
    public static class ShopRepository
    {
        public static async Task<IList<Product>> GetAllProductsAsync()
        {
            HttpClient client = new HttpClient();
            string json = await client.GetStringAsync(new Uri($"http://localhost:49681/api/Shop/get_products"));
            IList<Product> products = JsonConvert.DeserializeObject<ObservableCollection<Product>>(json);
            return products;
        }

        public static async Task<Order> PostOrder(Order order)
        {
            PostOrderDTO orderDTO = PostOrderDTO.FromOrder(order);
            string content = JsonConvert.SerializeObject(orderDTO);

            UserService serv = UserService.GetInstance();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", serv.Token));
            HttpResponseMessage res = await client.PostAsync(new Uri($"http://localhost:49681/api/Shop/create_order/{serv.User.FlightId}"),
                new HttpStringContent(content, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));
            
            if (res.IsSuccessStatusCode)
            {
                PostOrderResponseDTO response = JsonConvert.DeserializeObject<PostOrderResponseDTO>(res.Content.ToString());
                return response.ToOrder();
            }
            return null;
        }

        #region DTOs
        //Send
        private class PostOrderDTO
        {
            public IList<OrderLineDTO> OrderLines { get; set; }

            public static PostOrderDTO FromOrder(Order order)
            {
                return new PostOrderDTO { OrderLines = order.OrderLines.Select(o => OrderLineDTO.FromOrderLine(o)).ToList() };
            }
        }
        //Receive
        private class PostOrderResponseDTO
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
        private class OrderLineDTO
        {
            public Product Product { get; set; }
            public int Amount { get; set; }

            public static OrderLineDTO FromOrderLine(OrderLine orderLine)
            {
                return new OrderLineDTO
                {
                    Product = orderLine.Product,
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
        private class PassengerDTO
        {
            public int Id { get; set; }
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public DateTime BirthDate { get; set; }
            public SeatDTO Seat { get; set; }

            public Passenger ToPassenger()
            {
                return new Passenger
                {
                    Id = this.Id,
                    LastName = this.LastName,
                    FirstName = this.FirstName,
                    BirthDate = this.BirthDate,
                    Seat = this.Seat.ToSeat()
                };
            }
        }
        private class SeatDTO
        {
            public int Id { get; set; }
            public int Nr { get; set; }
            public ClassType ClassType { get; set; }

            public Seat ToSeat()
            {
                return new Seat { Id = this.Id, Nr = this.Nr, ClassType = this.ClassType };
            }
        }
        #endregion
    }
}
