using System.Collections.Generic;

namespace FlightApp.Model
{
    public class Passenger : ApplicationUser
    {
        public IList<PassengerFlight> PassengerFlights { get; set; }

        public Passenger()
        {
            PassengerFlights = new List<PassengerFlight>();
        }
    }
}
