using System.Data.Entity;
using System.Threading.Tasks;
using WebGigHub.Core.Models;
using WebGigHub.Core.Repositories;

namespace WebGigHub.Persistence.Repositories
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly IApplicationDbContext _context;

        public FollowingRepository(IApplicationDbContext context) => _context = context;

        public Task<bool> IsFollowing(string artistId, string userId)
        {
            return _context.Followings.AnyAsync(following => following.FolloweeId == artistId && following.FollowerId == userId);
        }
    }
}