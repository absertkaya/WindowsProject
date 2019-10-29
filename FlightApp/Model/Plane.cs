using System.ComponentModel.DataAnnotations;

namespace FlightApp.Model
{
    public class Plane
    {
        private decimal _price;

        public int ProductId { get; set; }
        [Required]
        [MaxLength(100)]
        public string ProductName { get; set; }
        [Required]
        public decimal Price
        {
            get { return _price; }
            set { if (value > 0) _price = value; }
        }
        [Required]
        public int AmountInStock { get; set; }
        [Required]
        public ProductType ProductType { get; set; }
    }
}
