using System.Collections.Generic;
using System.Threading.Tasks;
using WebGigHub.Core.Models;

namespace WebGigHub.Core.Repositories
{
    public interface IFolloweeRepository
    {
        Task<List<ApplicationUser>> GetFolloweesByFollowerId(string id);
    }
}