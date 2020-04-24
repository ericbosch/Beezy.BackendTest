namespace Beezy.BackendTest.Domain.Entities
{
    public class MovieGenre
    {
        public int MovieId { get; set; }
        public int GenreId { get; set; }

        public MovieGenre()
        {

        }

        public MovieGenre(int movieId, int genreId)
        {
            MovieId = movieId;
            GenreId = genreId;
        }
    }
}
