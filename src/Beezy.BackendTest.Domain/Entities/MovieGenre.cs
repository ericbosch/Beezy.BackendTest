namespace Beezy.BackendTest.Domain.Entities
{
    public class MovieGenre
    {
        public int MovieId { get; }
        public int GenreId { get; }

        private MovieGenre()
        {

        }

        public MovieGenre(int movieId, int genreId)
        {
            MovieId = movieId;
            GenreId = genreId;
        }
    }
}
