using FlightApp.Model;
using System;

namespace FlightApp.Data.DTOs
{
    public class PassengerDTO
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime BirthDate { get; set; }
        public int SeatNr { get; set; }

        public Passenger ToPassenger()
        {
            return new Passenger
            {
                LastName = this.LastName,
                FirstName = this.FirstName,
                Seat = new Seat { Nr = SeatNr }
            };
        }
    }
}
