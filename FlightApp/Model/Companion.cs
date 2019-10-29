using System.ComponentModel.DataAnnotations;

namespace FlightApp.Model
{
    public class Companion
    {
        public int FlightId1 { get; set; }
        public int FlightId2 { get; set; }

        [Required]
        public PassengerFlight Flight1 { get; set; }
        [Required]
        public PassengerFlight Flight2 { get; set; }
    }
}
