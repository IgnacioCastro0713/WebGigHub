using System.Threading.Tasks;
using WebGigHub.Core;
using WebGigHub.Core.Models;
using WebGigHub.Core.Repositories;
using WebGigHub.Persistence.Repositories;

namespace WebGigHub.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGigRepository GigRepository { get; private set; }
        public IAttendanceRepository AttendanceRepository  { get; private set; }
        public IGenreRepository GenreRepository { get; private set; }
        public IFollowingRepository FollowingRepository { get; private set; }
        

        public UnitOfWork(ApplicationDbContext context)
        {
           _context = context;
           GigRepository = new GigRepository(_context);
           AttendanceRepository = new AttendanceRepository(_context);
           GenreRepository = new GenreRepository(_context);
           FollowingRepository = new FollowingRepository(_context);
        }

        public async Task Complete()
        {
            await _context.SaveChangesAsync();
        }
    }
}