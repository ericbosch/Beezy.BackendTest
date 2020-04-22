using System;
using System.Collections.Generic;
using System.Linq;

namespace Beezy.BackendTest.Domain.Queries.IntelligentBillboard.Models
{
    public class Movie
    {
        public double Popularity { get; set; }
        public int VoteCount { get; set; }
        public bool Video { get; set; }
        public string PosterPath { get; set; }
        public int Id { get; set; }
        public bool Adult { get; set; }
        public string BackdropPath { get; set; }
        public string OriginalLanguage { get; set; }
        public string OriginalTitle { get; set; }
        public List<int> GenreIds { get; set; }
        public List<string> Genres => GenreIds.Select(g => g.ToString()).ToList();
        public string Title { get; set; }
        public double VoteAverage { get; set; }
        public string Overview { get; set; }
        public DateTime ReleaseDate { get; set; }
        public ScreenSize ScreenSize;
    }

    public enum ScreenSize
    {
        Big,
        Small
    }
}
