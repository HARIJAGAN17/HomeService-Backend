using System.ComponentModel.DataAnnotations;

namespace HomeService.Microservice.API.Model
{
    public class CompletedOrder
    {
        [Key]
        public int CompletedId {  get; set; }

        public int BookingId { get; set; }
        public string? Date { get; set; }
        public string? Status { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerId { get; set; }
        public string? ProviderId { get; set; }
        public string? CustomerEmail { get; set; }
        public int ServiceId { get; set; }
        public string? ServiceName { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public float Price { get; set; }
        public int Experience { get; set; }
        public string? ProviderName { get; set; }
        public string? ProviderEmail { get; set; }
        public string? Location { get; set; }
    }
}
