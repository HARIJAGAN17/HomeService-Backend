using HomeService.Microservice.API.Dto;
using HomeService.Microservice.API.Model;

namespace HomeService.Microservice.API.Contracts
{
    public interface IServicesRepo
    {
        public Task<IEnumerable<Service>> GetAllServices();

        public Task<Service> AddService(AddServiceDto service);

        public Task<bool> DeleteService(int id);

        public Task<Service> UpdateService(int id,UpdateServiceDto service);
    }
}
