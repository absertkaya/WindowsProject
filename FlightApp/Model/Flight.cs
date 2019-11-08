using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlightApp.Model
{
    public class Flight
    {
        public int Id { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        [MaxLength(200)]
        public string DepartureDest { get; set; }
        [MaxLength(200)]
        public string ArrivalDest { get; set; }

        public IList<Announcement> Announcements { get; set; }
        public IList<Seat> Seats { get; set; }
        public IList<Staff> Staff { get; set; }
        public IList<Order> Orders { get; set; }
    }
}
