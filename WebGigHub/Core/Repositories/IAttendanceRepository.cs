using System.Collections.Generic;
using System.Threading.Tasks;
using WebGigHub.Core.Models;

namespace WebGigHub.Core.Repositories
{
    public interface IAttendanceRepository
    {
        IEnumerable<Attendance> GetFutureAttendances(string userId);
        Task<bool> IsAttending(int gigId, string userId);
        Task<Attendance> GetAttendance(string userId, int gigId);
        void Add(Attendance attendance);
        void Remove(Attendance attendance);
    }
}