using HomeService.Microservice.API.Contracts;
using HomeService.Microservice.API.Dto;
using HomeService.Microservice.API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeService.Microservice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServicesRepo _serviceRepo;
        public ServiceController(IServicesRepo servicesRepo)
        {
            _serviceRepo = servicesRepo;
        }


        [HttpGet]
        [Authorize(Roles = "Provider,Customer")]
        public async Task<IActionResult> GetAllServices() {

            return Ok(await _serviceRepo.GetAllServices());

        }

        [HttpPost]
        [Authorize(Roles = "Provider")]
        public async Task<IActionResult> AddServices([FromBody] AddServiceDto service)
        {
            return Ok(await _serviceRepo.AddService(service));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Provider")]

        public async Task<IActionResult> DeleteService([FromRoute] int id)
        {
            var isDeleted = await _serviceRepo.DeleteService(id);
            if (isDeleted == true)
            {
                return Ok(new {response="deleted"});
            }
            return Ok(new { response = "notexist"});
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Provider")]

        public async Task<IActionResult> UpdateService([FromRoute]int id,[FromBody] UpdateServiceDto service)
        {
            var isUpdated = await _serviceRepo.UpdateService(id, service);
            if (isUpdated != null)
            {
                return Ok(new { response = "Updated" });
            }
            return Ok(new { response = "notexist" });
        }
    }
}
