using HomeService.Microservice.API.Dto;
using HomeService.Microservice.API.Model;

namespace HomeService.Microservice.API.Contracts
{
    public interface ICompletedRepo
    {
        Task<IEnumerable<CompletedOrder>> GetAllCompletedOrders();
        Task<CompletedOrder> AddCompletedOrder(AddCompletedOrderDto completedOrderDto);
    }
}
