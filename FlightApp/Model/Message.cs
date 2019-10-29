using System;
using System.ComponentModel.DataAnnotations;

namespace FlightApp.Model
{
    public class Message
    {
        [Required]
        public PassengerFlight Sender { get; set; }
        [Required]
        public PassengerFlight Receiver { get; set; }
        
        public int MessageId { get; set; }
        [Required]
        [MaxLength(255)]
        public string Content { get; set; }
        [Required]
        public DateTime Timestamp { get; set; }

        public Message()
        {
            Timestamp = DateTime.Now;
        }
    }
}
