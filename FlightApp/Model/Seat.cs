using System.ComponentModel.DataAnnotations;

namespace FlightApp.Model
{
    public class Seat
    {
        public int SeatId { get; set; }
        public int SeatNr { get; set; }
        [Required]
        public ClassType ClassType { get; set; }
        [Required]
        public PassengerFlight Passenger { get; set; }
    }
}
