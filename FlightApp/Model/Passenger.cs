using System.Collections.Generic;

namespace FlightApp.Model
{
    public class Passenger : ApplicationUser
    {
        public Seat Seat { get; set; }
        public IList<Order> Orders { get; set; }
        public IList<Message> SentMessages { get; set; }
        public IList<Message> ReceivedMessages { get; set; }
        public IList<Friend> Friends { get; set; }
    }
}
