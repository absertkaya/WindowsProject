namespace FlightApp.Model
{
    public class OrderLine
    {
        public Order Order { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }
    }
}