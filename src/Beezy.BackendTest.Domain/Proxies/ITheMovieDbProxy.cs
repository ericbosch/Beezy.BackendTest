using System.Collections.Generic;
using System.Threading.Tasks;
using Beezy.BackendTest.Domain.Queries.IntelligentBillboard.Models;
using Optional;

namespace Beezy.BackendTest.Domain.Proxies
{
    public interface ITheMovieDbProxy
    {
        public Task<Option<List<Movie>>> GetMovies();
    }
}
