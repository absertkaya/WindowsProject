using System;
using System.ComponentModel.DataAnnotations;

namespace FlightApp.Model
{
    public class Flight
    {
        public int FlightId { get; set; }
        [Required]
        public DateTime DepartureTime { get; set; }
        [Required]
        public DateTime ArrivalTime { get; set; }
        [Required]
        [MaxLength(200)]
        public string DepartureDest { get; set; }
        [Required]
        [MaxLength(200)]
        public string ArrivalDest { get; set; }
    }
}
