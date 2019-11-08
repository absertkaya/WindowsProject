using System;
using System.ComponentModel.DataAnnotations;

namespace FlightApp.Model
{
    public class Message
    {
        public Passenger Sender { get; set; }
        public Passenger Receiver { get; set; }
        
        public int Id { get; set; }
        [MaxLength(255)]
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
