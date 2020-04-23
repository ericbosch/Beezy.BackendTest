using System;
using System.Collections.Generic;

namespace Beezy.BackendTest.Domain.Queries.IntelligentBillboard.Models
{
    public class MovieInfo
    {

        public static readonly ScreenSize BigScreen = ScreenSize.Create("Big");
        public static readonly ScreenSize SmallScreen = ScreenSize.Create("Small");

        public string Title { get; }
        public string Overview { get; }
        public IReadOnlyList<string> Genres { get; }
        public string Language { get; }
        public DateTime ReleaseDate { get; }
        public int SeatsSold { get; }
        public ScreenSize ScreenSize { get; }

        private MovieInfo(
            string title,
            string overview,
            IReadOnlyList<string> genres,
            string language,
            DateTime releaseDate,
            int seatsSold,
            ScreenSize screenSize)
        {
            Title = title;
            Overview = overview;
            Genres = genres;
            Language = language;
            ReleaseDate = releaseDate;
            SeatsSold = seatsSold;
            ScreenSize = screenSize;
        }
        public static MovieInfo Create(string title, string overview, IReadOnlyList<string> genres, string language, DateTime releaseDate, int seatsSold, ScreenSize screenSize)
        {
            return new MovieInfo(title, overview, genres, language, releaseDate, seatsSold, screenSize);
        }
    }
}
