using System.Collections.Generic;
using System.Threading.Tasks;
using WebGigHub.Core.Models;

namespace WebGigHub.Core.Repositories
{
    public interface IGigRepository
    {
        IEnumerable<Gig> GetUpcomingGigsByArtist(string artistId);

        IEnumerable<Gig> GetGigsUserAttending(string userId);
        Task<Gig> GetGigWithAttendances(int gigId);
        Task<List<Gig>> GetUpcomingGigs(string userId);
        Task<Gig> GetGig(int id);
        Task<Gig> GetGigDetail(int id);
        void Add(Gig gig);
    }
}