
using HomeService.Microservice.API.Contracts;
using HomeService.Microservice.API.Data;
using HomeService.Microservice.API.Dto;
using HomeService.Microservice.API.Model;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace HomeService.Microservice.API.Repository
{
    public class ServiceRepository : IServicesRepo
    {
        private readonly ApplicationDbContext _dbcontext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ServiceRepository(ApplicationDbContext context,IHttpContextAccessor httpContextAccessor)
        {
            _dbcontext = context;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<Service> AddService(AddServiceDto service)
        {

            var userId = _httpContextAccessor.HttpContext?.User.Claims
           .FirstOrDefault(c => c.Type == "UserId")?.Value;

            var userName = _httpContextAccessor.HttpContext?.User.Claims
           .FirstOrDefault(c => c.Type == "UserName")?.Value;

            var userEmail = _httpContextAccessor.HttpContext?.User.Claims
           .FirstOrDefault(c => c.Type == "UserEmail")?.Value;

            //convert dto to service model

            var currentService = new Service
            {
                Experience = service.Experience,
                Category  = service.Category,
                ServiceName = service.ServiceName,
                Price = service.Price,
                Description = service.Description,
                Location = service.Location,
            };

            // Assign the Provider details from claims
            currentService.ProviderId = userId;
            currentService.ProviderName=userName;
            currentService.ProviderEmail = userEmail;

           var ServiceAdded = await _dbcontext.Services.AddAsync(currentService);
           await _dbcontext.SaveChangesAsync();
           return currentService;
        }

        public async Task<bool> DeleteService(int id)
        {
            var serviceExist = _dbcontext.Services.FirstOrDefault(x => x.ServiceId == id);
            if(serviceExist!=null)
            {
                _dbcontext.Services.Remove(serviceExist);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Service>> GetAllServices()
        {
           return await _dbcontext.Services.ToListAsync();
        }

        public async Task<Service> UpdateService(int id, UpdateServiceDto service)
        {
            var currService = _dbcontext.Services.FirstOrDefault(x=>x.ServiceId == id);

            if(currService!=null)
            {
                currService.Experience = service.Experience;
                currService.Category = service.Category;
                currService.ServiceName = service.ServiceName;
                currService.Price = service.Price;
                currService.Description = service.Description;
                currService.Location = service.Location;
                await _dbcontext.SaveChangesAsync();
                return currService;
            }
            return null;
        }
    }
}
