using System.Threading.Tasks;
using WebGigHub.Core.Repositories;

namespace WebGigHub.Core
{
    public interface IUnitOfWork
    {
        IGigRepository GigRepository { get; }
        IAttendanceRepository AttendanceRepository { get; }
        IGenreRepository GenreRepository { get; }
        IFollowingRepository FollowingRepository { get; }
        IFolloweeRepository FolloweeRepository { get; }
        Task Complete();
    }
}