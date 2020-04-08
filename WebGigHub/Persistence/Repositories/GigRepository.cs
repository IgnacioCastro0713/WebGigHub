using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WebGigHub.Core.Models;
using WebGigHub.Core.Repositories;

namespace WebGigHub.Persistence.Repositories
{
    public class GigRepository : IGigRepository
    {
        private readonly IApplicationDbContext _context;

        public GigRepository(IApplicationDbContext context) => _context = context; // change

        public IEnumerable<Gig> GetUpcomingGigsByArtist(string artistId)
        {
            return _context.Gigs
                .Where(g =>
                    g.ArtistId == artistId &&
                    g.DateTime > DateTime.Now &&
                    !g.IsCanceled)
                .Include(g => g.Genre)
                .ToList();
        }
        
        public IEnumerable<Gig> GetGigsUserAttending(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();
        }

        public async Task<Gig> GetGigWithAttendances(int gigId)
        {
            return await _context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .SingleOrDefaultAsync(g => g.Id == gigId);
        }

        public async Task<List<Gig>> GetUpcomingGigs(string userId)
        {
            return await _context.Gigs
                .Where(g => g.ArtistId == userId && g.DateTime > DateTime.Now && !g.IsCanceled)
                .Include(g => g.Genre)
                .ToListAsync();
        }

        public async Task<Gig> GetGig(int id)
        {
            return await _context.Gigs.SingleAsync(g => g.Id == id);
        }

        public async Task<Gig> GetGigDetail(int id)
        {
            return await _context.Gigs
                .Include(g => g.Genre)
                .Include(g => g.Artist)
                .SingleOrDefaultAsync(g => g.Id == id);
        }

        public void Add(Gig gig)
        {
            _context.Gigs.Add(gig);
        }
    }
}