using HomeService.Microservice.API.Contracts;
using HomeService.Microservice.API.Data;
using HomeService.Microservice.API.Dto;
using HomeService.Microservice.API.Model;
using Microsoft.EntityFrameworkCore;

namespace HomeService.Microservice.API.Repository
{
    public class BookingRepository : IBookingRepo
    {
        private readonly ApplicationDbContext _dbcontext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BookingRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _dbcontext = context;
            _httpContextAccessor = httpContextAccessor;

        }
        public async Task<Booking> AddBooking(AddBooking booking)
        {

            var userId = _httpContextAccessor.HttpContext?.User.Claims
            .FirstOrDefault(c => c.Type == "UserId")?.Value;
            var userName = _httpContextAccessor.HttpContext?.User.Claims
            .FirstOrDefault(c => c.Type == "UserName")?.Value;
            var userEmail = _httpContextAccessor.HttpContext?.User.Claims
            .FirstOrDefault(c => c.Type == "UserEmail")?.Value;

            var currBooking = new Booking()
            {
                Date = booking.Date,
                ProviderId = booking.ProviderId,
                ServiceId = booking.ServiceId,
                
            };
            
            currBooking.CustomerName = userName;
            currBooking.CustomerId= userId;
            currBooking.CustomerEmail = userEmail;

            await _dbcontext.Bookings.AddAsync(currBooking);
            await _dbcontext.SaveChangesAsync();
            return currBooking;
        }

        public async Task<IEnumerable<Booking>> GetALlBooking()
        {
            return await _dbcontext.Bookings.ToListAsync();
        }

        public async Task<bool> DeleteBooking(int id)
        {
            var BookingExist= await _dbcontext.Bookings.FirstOrDefaultAsync(x=>x.BookingId==id);
            if (BookingExist != null)
            {
                _dbcontext.Bookings.Remove(BookingExist);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        

        public async Task<Booking> UpdateBooking(int id, UpdateStatusDto updateStatusDto)
        {
            var BookingExist = await _dbcontext.Bookings.FirstOrDefaultAsync(x => x.BookingId == id);
            if (BookingExist != null)
            {
                BookingExist.Status = updateStatusDto.Status;
                await _dbcontext.SaveChangesAsync();
                return BookingExist;
            }
            return null;

        }
    }
}
