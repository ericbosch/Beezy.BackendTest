using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Beezy.BackendTest.Domain.Entities;
using Beezy.BackendTest.Domain.Queries.IntelligentBillboard.Models;
using Beezy.BackendTest.Domain.Repositories;
using Beezy.BackendTest.Infrastructure.Data.DbContext;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Beezy.BackendTest.Infrastructure.Data.Repositories
{
    public class BeezyCinemaRepository : IBeezyCinemaRepository
    {
        private readonly BeezyCinemaContext _context;

        public BeezyCinemaRepository(BeezyCinemaContext context)
        {
            _context = context;
        }

        public async Task<Option<List<MovieInfo>>> GetMovies()
        {
            var movies = await _context.Movie
                .Include(m => m.Session)
                .ThenInclude(s => s.Room)
                .ToListAsync();

            var genresByMovie = await _context.MovieGenre.ToListAsync();
            var genres = await _context.Genre.ToListAsync();

            return GetMovieInfo(movies, genres, genresByMovie).ToList().SomeNotNull();
        }

        private IEnumerable<MovieInfo> GetMovieInfo(List<Movie> movies, List<Genre> genres, List<MovieGenre> genresByMovie)
        {
            var aux = movies.SelectMany(m =>
                m.Session.Select(s => new {movie = m, seats = s.SeatsSold, size = s.Room?.Size}));
            var auxGrouped = aux.GroupBy(m => new {m.movie, m.size}, m => m.seats.Value,
                (movie, seats) => new {movieWithSeats = movie, seatsSold = seats.Sum()});
            return movies.SelectMany(m =>
                    m.Session.Select(s => new { movie = m, seats = s.SeatsSold, size = s.Room?.Size }))
                .GroupBy(m => new { m.movie, m.size }, m => m.seats.Value,
                    (size, seats) => new { movieWithSeats = size, seatsSold = seats.Sum() })
                .Select(movieItem => MovieInfo.Create(movieItem.movieWithSeats.movie.OriginalTitle,
                    string.Empty,
                    GetMoviesGenre(genresByMovie, genres, movieItem.movieWithSeats.movie.Id),
                    movieItem.movieWithSeats.movie.OriginalLanguage,
                    movieItem.movieWithSeats.movie.ReleaseDate,
                    movieItem.seatsSold,
                    ScreenSize.Create(movieItem.movieWithSeats.size)));
        }

        private IReadOnlyList<string> GetMoviesGenre(List<MovieGenre> genresByMovie, List<Genre> genres, int movieId)
        {
            var movieGenresById = genresByMovie
                .Where(gm => gm.MovieId == movieId)
                .Select(gm => gm.GenreId);
            return genres.Where(g => movieGenresById.Contains(g.Id))
                .Select(g => g.Name).ToList();
        }
    }
}
