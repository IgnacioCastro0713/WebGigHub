using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WebGigHub.Core.Models;
using WebGigHub.Core.Repositories;

namespace WebGigHub.Persistence.Repositories
{
    public class FolloweeRepository: IFolloweeRepository
    {

        private readonly IApplicationDbContext _context;

        public FolloweeRepository(IApplicationDbContext context) => _context = context;

        public async Task<List<ApplicationUser>> GetFolloweesByFollowerId(string id)
        {
            return await _context.Followings
                .Where(following => following.FollowerId == id)
                .Select(f => f.Followee)
                .ToListAsync();
        }
        
    }
}