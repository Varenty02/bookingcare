using bookingcare.Models;

namespace bookingcare.Repositories
{
    public interface IBookingRepository
    {
        Task<IEnumerable<BookingModel>> GetAllBookings();
        Task<BookingModel> GetBookingById(string id);
        Task<int> RegisterBooking(BookingModel model);
        Task<int> CancelBooking(string userId, int scheduleId);
    }
}
