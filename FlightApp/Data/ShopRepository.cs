using FlightApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;

namespace FlightApp.Data
{
    public static class ShopRepository
    {
        public static async Task<IList<Product>> GetAllProductsAsync()
        {
            HttpClient client = new HttpClient();
            string json = await client.GetStringAsync(new Uri($"http://localhost:49681/api/Flight/get_products"));
            IList<Product> products = JsonConvert.DeserializeObject<ObservableCollection<Product>>(json);
            return products;
        }
    }
}
