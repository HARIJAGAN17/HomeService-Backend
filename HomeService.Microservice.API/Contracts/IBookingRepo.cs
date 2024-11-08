using HomeService.Microservice.API.Dto;
using HomeService.Microservice.API.Model;

namespace HomeService.Microservice.API.Contracts
{
    public interface IBookingRepo
    {

        Task<IEnumerable<Booking>> GetALlBooking();

        Task<Booking> AddBooking(AddBooking booking);

        Task<Booking> UpdateBooking(int id,UpdateStatusDto updateStatusDto);

        Task<bool> DeleteBooking(int id);
    }
}
