using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlightApp.Model
{
    public class PassengerFlight
    {
        public int PassengerFlightId { get; set; }

        [Required]
        public Passenger Passenger { get; set; }
        [Required]
        public Flight Flight { get; set; }
        [Required]
        public Seat Seat { get; set; }

        public IList<Message> SentMessages { get; set; }
        public IList<Message> ReceivedMessages { get; set; }
        public IList<Order> PlacedOrders { get; set; }

        public PassengerFlight()
        {
            SentMessages = new List<Message>();
            ReceivedMessages = new List<Message>();
            PlacedOrders = new List<Order>();
        }
    }
}
