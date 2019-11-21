﻿using FlightApp.Data.DTOs;
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
                OrderDTO response = JsonConvert.DeserializeObject<OrderDTO>(res.Content.ToString());
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
        #endregion
    }
}
