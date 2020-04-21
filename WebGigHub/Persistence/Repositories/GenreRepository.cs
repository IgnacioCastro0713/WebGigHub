using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using WebGigHub.Core.Models;
using WebGigHub.Core.Repositories;

namespace WebGigHub.Persistence.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        
        private readonly IApplicationDbContext _context;
        
        public GenreRepository(IApplicationDbContext context) => _context = context;

        public Task<List<Genre>> GetGenres()
        {
            return _context.Genres.ToListAsync();
        }
    }
}