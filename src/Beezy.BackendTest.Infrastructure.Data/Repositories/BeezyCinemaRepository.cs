using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

        public async Task<Option<List<MovieInfo>>> GetMovies(string city)
        {
            var movies = await _context.Movie
                .Include(m => m.Session)
                .ThenInclude(s => s.Room)
                .ToListAsync();

            var cities = await _context.City
                .Include(c => c.Cinema)
                .ThenInclude(c => c.Room)
                .ToListAsync();

            var genresByMovie = await _context.MovieGenre.ToListAsync();
            var genres = await _context.Genre.ToListAsync();

            movies = GetMoviesProjectedAtCity(movies, cities, city);

            return GetMovieInfo(movies, genres, genresByMovie).ToList().SomeNotNull();
        }

        private List<Movie> GetMoviesProjectedAtCity(List<Movie> movies, List<City> cities, string city)
        {
            if (string.Equals(city, "all", StringComparison.CurrentCultureIgnoreCase)) return movies;

            var selectedCity = cities.FirstOrDefault(c => string.Equals(c.Name, city, StringComparison.CurrentCultureIgnoreCase));
            if (selectedCity == null) return new List<Movie>();
            var roomsInCinemas = selectedCity.Cinema.Select(c => c.Room).SelectMany(r => r).Distinct();

            var moviesWithRooms = movies.SelectMany(m => 
                m.Session.Select(s => new {movie = m, room = s.Room}))
                .Where(movie => roomsInCinemas!.Contains(movie.room)).Distinct();
            return moviesWithRooms.Select(m => m.movie).ToList();
        }

        private IEnumerable<MovieInfo> GetMovieInfo(List<Movie> movies, List<Genre> genres, List<MovieGenre> genresByMovie)
        {
            return movies.SelectMany(m =>
                    m.Session.Select(s => new { movie = m, seats = s.SeatsSold, size = s.Room?.Size }))
                .GroupBy(m => new { m.movie, m.size }, m => m.seats ?? 0,
                    (size, seats) => new { movieWithSeats = size, seatsSold = seats.Sum() })
                .OrderByDescending(m => m.seatsSold)
                .ThenByDescending(m => m.movieWithSeats.movie.ReleaseDate)
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
