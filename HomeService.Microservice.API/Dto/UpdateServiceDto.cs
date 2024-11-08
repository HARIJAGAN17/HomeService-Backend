namespace HomeService.Microservice.API.Dto
{
    public class UpdateServiceDto
    {
        
        public string? ServiceName { get; set; }

        public string? Category { get; set; }

        public string? Description { get; set; }

        public float Price { get; set; }
        public int Experience { get; set; }

        public string? Location { get; set; }
    }
}
