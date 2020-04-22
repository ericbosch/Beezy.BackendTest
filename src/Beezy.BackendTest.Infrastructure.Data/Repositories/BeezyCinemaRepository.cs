using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beezy.BackendTest.Domain.Entities;
using Beezy.BackendTest.Domain.Repositories;
using Beezy.BackendTest.Infrastructure.Data.DbContext;
using Microsoft.EntityFrameworkCore;
using Optional;
using Movie = Beezy.BackendTest.Domain.Queries.IntelligentBillboard.Models.Movie;

namespace Beezy.BackendTest.Infrastructure.Data.Repositories
{
    class BeezyCinemaRepository : IBeezyCinemaRepository
    {
        private readonly BeezyCinemaContext _context;

        public BeezyCinemaRepository(BeezyCinemaContext context)
        {
            _context = context;
        }

        public Task<Option<List<Movie>>> GetMovies()
        {
            throw new NotImplementedException();
        }
    }
}
