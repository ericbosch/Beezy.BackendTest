using System.Collections.Generic;
using System.Threading.Tasks;
using Beezy.BackendTest.Domain.Queries.IntelligentBillboard.Models;
using Optional;

namespace Beezy.BackendTest.Domain.Repositories
{
    public interface IBeezyCinemaRepository
    {
        public Task<Option<List<MovieInfo>>> GetMovies(string city);
    }
}
