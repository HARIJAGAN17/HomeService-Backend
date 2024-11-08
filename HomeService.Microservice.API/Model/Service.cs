using System.ComponentModel.DataAnnotations;

namespace HomeService.Microservice.API.Model
{
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }

        public string? ServiceName { get; set; }

        public string? Category { get; set; }

        public string? Description { get; set; }

        public float Price { get; set; }
        public int Experience { get; set; }
        public string? ProviderName { get; set; }

        public string? ProviderId { get; set; }

        public string? ProviderEmail { get; set; }

        public string? Location {  get; set; }


    }
}
