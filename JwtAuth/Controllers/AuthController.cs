using JwtAuth.Contracts;
using JwtAuth.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _auth;

        public AuthController(IAuth auth)
        {
            _auth = auth;
        }

        [HttpPost]
        [Route("register-customer")]
        public async Task<IActionResult> CustomerRegister([FromBody] Register model)
        {
            var userExist = await _auth.CustomerRegister(model);
            return Ok(userExist);
        }

        [HttpPost]
        [Route("register-provider")]
        public async Task<IActionResult> ProviderRegister([FromBody] Register model)
        {
            var userExist = await _auth.ServiceProviderRegister(model);
            return Ok(userExist);
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> AdminRegister([FromBody] Register model)
        {
            var userExist = await _auth.AdminRegister(model);
            return Ok(userExist);
        }


        //login

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            var user = await _auth.CheckLoginCred(model);
            if (user != null)
            {
                return Ok(user);
            }
            return Unauthorized();
        }



    }
}
