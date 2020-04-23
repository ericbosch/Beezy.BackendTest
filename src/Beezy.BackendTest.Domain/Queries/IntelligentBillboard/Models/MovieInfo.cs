using System;
using System.Collections.Generic;

namespace Beezy.BackendTest.Domain.Queries.IntelligentBillboard.Models
{
    public class MovieInfo
    {
        public static readonly ScreenSize BigScreen = ScreenSize.Create("BigScreen");
        public static readonly ScreenSize SmallScreen = ScreenSize.Create("SmallScreen");

        public string Title { get; }
        public string Overview { get; }
        public IReadOnlyList<string> Genres { get; }
        public string Language { get; }
        public DateTime ReleaseDate { get; }
        public int SeatsSold { get; }
        public ScreenSize ScreenSize { get; }

        public MovieInfo(
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
    }
}
