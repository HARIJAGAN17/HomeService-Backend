using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeService.Microservice.API.Model
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        public string? Date { get; set; } 
        public string? Status { get; set; } = "Pending";

        public string? CustomerName {  get; set; }
        public string? CustomerId { get; set; }

        public string? CustomerEmail { get; set; }


        public string? ProviderId { get; set; }

        public int ServiceId { get; set; }

    }
}
