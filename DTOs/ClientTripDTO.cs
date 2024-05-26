namespace TripEF.DTOs
{
    public class ClientTripDTO
    {
        public string Pesel { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}