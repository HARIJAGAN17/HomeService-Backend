using HomeService.Microservice.API.Model;
using Microsoft.EntityFrameworkCore;

namespace HomeService.Microservice.API.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<CompletedOrder> CompletedOrders { get; set; }
    }
}
