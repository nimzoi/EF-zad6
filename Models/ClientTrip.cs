﻿using EfExample.Models;

namespace TripEF.Models
{
    public class ClientTrip
    {
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int TripId { get; set; }
        public Trip Trip { get; set; }
        public DateTime RegisteredAt { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}