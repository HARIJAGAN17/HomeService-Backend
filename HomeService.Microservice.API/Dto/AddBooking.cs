namespace HomeService.Microservice.API.Dto
{
    public class AddBooking
    {
        public string? Date { get; set; }

        public string? ProviderId { get; set; }

        public int ServiceId { get; set; }
    }
}
