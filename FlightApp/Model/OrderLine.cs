using System.ComponentModel.DataAnnotations;

namespace FlightApp.Model
{
    public class OrderLine
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        [Required]
        public Order Order { get; set; }
        [Required]
        public Product Product { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}