using HomeService.Microservice.API.Contracts;
using HomeService.Microservice.API.Data;
using HomeService.Microservice.API.Dto;
using HomeService.Microservice.API.Model;
using Microsoft.EntityFrameworkCore;

namespace HomeService.Microservice.API.Repository
{
    public class CompletedOrderRepository : ICompletedRepo
    {
        private readonly ApplicationDbContext _dbcontext;
        public CompletedOrderRepository(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        public async Task<CompletedOrder> AddCompletedOrder(AddCompletedOrderDto completedOrder)
        {
            var currOrder = new CompletedOrder
            {
                BookingId = completedOrder.BookingId,
                Date = completedOrder.Date,
                Status = completedOrder.Status,
                CustomerName = completedOrder.CustomerName,
                CustomerId = completedOrder.CustomerId,
                ProviderId = completedOrder.ProviderId,
                CustomerEmail = completedOrder.CustomerEmail,
                ServiceId = completedOrder.ServiceId,
                ServiceName = completedOrder.ServiceName,
                Category = completedOrder.Category,
                Description = completedOrder.Description,
                Price = completedOrder.Price,
                Experience = completedOrder.Experience,
                ProviderName = completedOrder.ProviderName,
                ProviderEmail = completedOrder.ProviderEmail,
                Location = completedOrder.Location,
            };
            await _dbcontext.CompletedOrders.AddAsync(currOrder);
            await _dbcontext.SaveChangesAsync();
            return currOrder;
        }

        public async Task<IEnumerable<CompletedOrder>> GetAllCompletedOrders()
        {
            return await _dbcontext.CompletedOrders.ToListAsync();
        }
    }
}
