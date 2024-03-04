using AutoMapper;
using bookingcare.Data;
using bookingcare.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace bookingcare.Repositories
{
    public class BookingRepository:IBookingRepository
    {
        private readonly BookingCareContext _context;
        private readonly IMapper _mapper;

        public BookingRepository(BookingCareContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> RegisterBooking(BookingModel model)
        {
            var schedule = await _context.Schedules.FindAsync(model.ScheduleId);
            if (schedule != null && schedule.CurrentNumber < schedule.MaxNumber)
            {
                schedule.CurrentNumber++;
                _context.Entry(schedule).State = EntityState.Modified;
                var booking = _mapper.Map<Booking>(model);
                await _context.Bookings!.AddAsync(booking);
                await _context.SaveChangesAsync();
                return StatusCodes.Status201Created;
            }
           
            return StatusCodes.Status400BadRequest;
        }
        public async Task<int> CancelBooking(string userId,int scheduleId)
        {
            var deleteBooking = _context.Bookings!.Where(b=>b.PatientId==userId&&b.ScheduleId==scheduleId).FirstOrDefault();
            if (deleteBooking != null)
            {
                _context.Bookings!.Remove(deleteBooking);
                //decrease current number
                var schedule = await _context.Schedules!.FindAsync(scheduleId);
                schedule.CurrentNumber--;
                _context.Entry(schedule).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return StatusCodes.Status200OK;
            }
            return StatusCodes.Status400BadRequest;
        }
        public async Task<IEnumerable<BookingModel>> GetAllBookings()
        {
            var bookings = await _context.Bookings!.ToListAsync();
            return _mapper.Map<IEnumerable<BookingModel>>(bookings);
        }

        public async Task<BookingModel> GetBookingById(string userId)
        {
            var booking = await _context.Bookings.Where(b => b.PatientId == userId )
                .FirstOrDefaultAsync();
            return _mapper.Map<BookingModel>(booking);
        }
    }
}
