using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WebGigHub.Core.Models;
using WebGigHub.Core.Repositories;

namespace WebGigHub.Persistence.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly IApplicationDbContext _context;

        public AttendanceRepository(IApplicationDbContext context) => _context = context;

        public IEnumerable<Attendance> GetFutureAttendances(string userId)
        {
            return _context.Attendances
                .Where(attendance => attendance.AttendeeId == userId && attendance.Gig.DateTime > DateTime.Now)
                .ToList();
        }

        public async Task<bool> IsAttending(int gigId, string userId)
        {
            return await _context.Attendances.AnyAsync(attendance =>
                attendance.GigId == gigId && attendance.AttendeeId == userId);
        }

        public async Task<Attendance> GetAttendance(string userId, int gigId)
        {
            return await _context.Attendances.SingleOrDefaultAsync(a => a.GigId == gigId && a.AttendeeId == userId);
        }

        public void Add(Attendance attendance)
        {
            _context.Attendances.Add(attendance);
        }

        public void Remove(Attendance attendance)
        {
            _context.Attendances.Remove(attendance);
        }
    }
}