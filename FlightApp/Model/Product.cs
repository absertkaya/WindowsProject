using System;
using System.ComponentModel.DataAnnotations;

namespace FlightApp.Model
{
    public class Product
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        public string Picture { get; set; }
        public decimal Price { get; set; }
        public ProductType Type { get; set; }

        public string PriceString { get { return String.Format("€{0:0.00}", Price); } }
    }
}
