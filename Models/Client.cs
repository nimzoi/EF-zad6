using System.Collections.Generic;

namespace TripEF.Models
{
    public class Client
    {
        public int ClientId { get; set; }
        public string Pesel { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<ClientTrip> ClientTrips { get; set; }
    }
}