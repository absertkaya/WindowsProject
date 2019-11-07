namespace FlightApp.Model
{
    public class Seat
    {
        public int Id { get; set; }
        public int Nr { get; set; }
        public ClassType ClassType { get; set; }
        public Passenger Passenger { get; set; }
        public Flight Flight { get; set; }
    }
}
