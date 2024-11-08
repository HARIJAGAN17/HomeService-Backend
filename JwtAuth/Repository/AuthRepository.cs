using JwtAuth.Contracts;
using JwtAuth.Data;
using JwtAuth.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuth.Repository
{
    public class AuthRepository : IAuth
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;


        public AuthRepository(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;

        }

        public async Task<Response> AdminRegister(Register model)
        {

            var userExist = await _userManager.FindByNameAsync(model.UserName);
            var emailExist = await _userManager.FindByEmailAsync(model.Email);
            if (userExist != null || emailExist != null)
            {
                return new Response { Status = "Failed", Message = "User already Exists sorry Login" };
            }

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName,
            };

            //creating user
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return new Response { Status = "passwordcondition", Message = "password must have one uppercase and special characters" };
            }

            //creating roles
            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }
            if (!await _roleManager.RoleExistsAsync(UserRoles.Customer))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Customer));
            }
            if (!await _roleManager.RoleExistsAsync(UserRoles.Provider))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Provider));
            }


            //assigning Roles

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                List<string> adminRoles = new List<string>()
                {
                   UserRoles.Admin, //admin have admin access
                   UserRoles.Customer, //admin have customer access
                   UserRoles.Provider //admin have service-provider access
                };
                await _userManager.AddToRolesAsync(user, adminRoles);
            }
            return new Response { Status = "Success", Message = "User Created Successfully" };

        }

       
        public async Task<Response> CustomerRegister(Register model)
        {

            var userExist = await _userManager.FindByNameAsync(model.UserName);
            var emailExist = await _userManager.FindByEmailAsync(model.Email);
            if (userExist != null || emailExist != null)
            {
                return new Response { Status = "Failed", Message = "User already Exists sorry Login" };
            }

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName,
            };

            //creating user
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return new Response { Status = "passwordcondition", Message = "password must have one uppercase and special characters" };
            }

            //creating roles if doesn't exist
            if (!await _roleManager.RoleExistsAsync(UserRoles.Customer))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Customer));
            }

            //assigning roles

            if (await _roleManager.RoleExistsAsync(UserRoles.Customer))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Customer); // customer have only customer access;
            }

            return new Response { Status = "Success", Message = "User created successfully!" };
        }

        public async Task<Response> ServiceProviderRegister(Register model)
        {

            var userExist = await _userManager.FindByNameAsync(model.UserName);
            var emailExist = await _userManager.FindByEmailAsync(model.Email);
            if (userExist != null || emailExist != null)
            {
                return new Response { Status = "Failed", Message = "User already Exists sorry Login" };
            }

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName,
            };

            //creating user
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return new Response { Status = "passwordcondition", Message = "password must have one uppercase and special characters" };
            }

            //creating roles if doesn't exist
            if (!await _roleManager.RoleExistsAsync(UserRoles.Provider))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Provider));
            }

            //assigning roles

            if (await _roleManager.RoleExistsAsync(UserRoles.Provider))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Provider); // provider have only provider access;
            }

            return new Response { Status = "Success", Message = "User created successfully!" };
        }
        public async Task<object> CheckLoginCred(Login model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("UserId",user.Id),
                    new Claim("UserName",user.UserName),
                    new Claim("UserEmail",user.Email),
                    new Claim("UserRole",userRoles.FirstOrDefault()??"NoRole"),

                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                var token = GetToken(authClaims);
                return new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                };
            }
            return null;
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTAuth:SecretKey"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWTAuth:ValidIssuerURL"],
                audience: _configuration["JWTAuth:ValidAudienceURL"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }

    }
}
