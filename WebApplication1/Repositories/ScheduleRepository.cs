using AutoMapper;
using bookingcare.Data;
using bookingcare.Helpers;
using bookingcare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace bookingcare.Repositories
{
    public class ScheduleRepository:IScheduleRepository
    {
        private readonly BookingCareContext _context;
        private readonly IMapper _mapper;

        public ScheduleRepository(BookingCareContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateSchedule(ScheduleCreateModel model)
        {
            var schedule = _mapper.Map<Schedule>(model);

            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();
            return schedule.Id;
        }
        public async Task<int> UpdateSchedule(int id, ScheduleModel model)
        {
            if (id != model.Id)
            {
                return StatusCodes.Status400BadRequest;
            }

            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return StatusCodes.Status404NotFound;
            }

            schedule.MaxNumber = model.MaxNumber;
            _context.Entry(schedule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ScheduleExists(id))
                {
                    return StatusCodes.Status404NotFound;
                }
                else
                {
                    throw;
                }
            }

            return StatusCodes.Status204NoContent;
        }
        public async Task<int> DeleteSchedule(int id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return StatusCodes.Status404NotFound;
            }

            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync();

            return StatusCodes.Status204NoContent;
        }

        public async Task<List<ScheduleModel>> GetAllSchedules()
        {
            var schedules = await _context.Schedules.ToListAsync();

            return _mapper.Map<List<ScheduleModel>>(schedules);
        }

        public async Task<ScheduleModel> GetScheduleById(int id)
        {
            var schedule = await _context.Schedules.FindAsync(id);

            if (schedule == null)
            {
                return null;
            }

            
            return _mapper.Map<ScheduleModel>(schedule);
        }

        public async Task<bool> ScheduleExists(int id)
        {
            return _context.Schedules.Any(e => e.Id == id);
        }
    }
}
