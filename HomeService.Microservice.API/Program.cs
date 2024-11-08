using HomeService.Microservice.API.Contracts;
using HomeService.Microservice.API.Data;
using HomeService.Microservice.API.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

ConfigurationManager configuration = builder.Configuration;

//sql db

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationConnection"));
});

//authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["JWTAuth:ValidAudienceURL"],
        ValidIssuer = configuration["JWTAuth:ValidIssuerURL"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTAuth:SecretKey"]))
    };
});


//add scope

builder.Services.AddScoped<IServicesRepo,ServiceRepository>();
builder.Services.AddScoped<IBookingRepo,BookingRepository>();
builder.Services.AddScoped<ICompletedRepo,CompletedOrderRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("HomeservicePolicy", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});
//httpcontextaccessor

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors("HomeservicePolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
