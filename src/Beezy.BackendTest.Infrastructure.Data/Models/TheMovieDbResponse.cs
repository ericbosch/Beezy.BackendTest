using System;
using System.Collections.Generic;

namespace Beezy.BackendTest.Infrastructure.Data.Models
{
    internal class TheMovieDbResponse
    {
        public int Page { get; set; }
        public int TotalResults { get; set; }
        public int TotalPages { get; set; }
        public List<Result> Results { get; set; }
    }

    public class Result
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
        public string Title { get; set; }
        public double VoteAverage { get; set; }
        public string Overview { get; set; }
        public DateTime ReleaseDate { get; set; }
    }

}