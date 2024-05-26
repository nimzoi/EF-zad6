using System;
using System.Collections.Generic;
using TripEF.Models;

namespace EfExample.Models
{
    public class Trip
    {
        public int TripId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<ClientTrip> ClientTrips { get; set; }
    }
}