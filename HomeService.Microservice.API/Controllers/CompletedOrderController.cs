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
    public class CompletedOrderController : ControllerBase
    {
        private readonly ICompletedRepo _completedRepo;
        public CompletedOrderController(ICompletedRepo completedRepo)
        {
            _completedRepo = completedRepo;
        }

        [Authorize(Roles ="Provider,Customer")]
        [HttpGet]

        public async Task<IActionResult> GetAllcompletedOrders() {
        
          return Ok(await _completedRepo.GetAllCompletedOrders());
        }


        [Authorize(Roles ="Provider")]
        [HttpPost]

        public async Task<IActionResult> AddCompletedOrder(AddCompletedOrderDto completedOrder) { 

            return Ok(await _completedRepo.AddCompletedOrder(completedOrder));
        }
    }
}
