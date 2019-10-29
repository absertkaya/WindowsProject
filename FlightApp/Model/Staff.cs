using System.Collections.Generic;

namespace FlightApp.Model
{
    public class Staff : ApplicationUser
    {
        public IList<Order> HandledOrders { get; set; }
        public IList<StaffFlight> StaffFlights { get; set; }

        public Staff()
        {
            HandledOrders = new List<Order>();
            StaffFlights = new List<StaffFlight>();
        }
    }
}
