using System.Threading.Tasks;

namespace WebGigHub.Core.Repositories
{
    public interface IFollowingRepository
    {
        Task<bool> IsFollowing(string artistId, string userId);
    }
}