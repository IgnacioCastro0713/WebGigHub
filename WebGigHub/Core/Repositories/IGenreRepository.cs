using System.Collections.Generic;
using System.Threading.Tasks;
using WebGigHub.Core.Models;

namespace WebGigHub.Core.Repositories
{
    public interface IGenreRepository
    {
        Task<List<Genre>> GetGenres();
    }
}