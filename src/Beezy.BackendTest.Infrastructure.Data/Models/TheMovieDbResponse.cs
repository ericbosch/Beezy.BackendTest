using System;
using Newtonsoft.Json;

namespace Beezy.BackendTest.Infrastructure.Data.Models
{

    public class TheMovieDbResponse
    {
        public Result[] Results { get; set; }
    }

    public class Result
    {
        public string Title { get; set; }
        public string Overview { get; set; }
        [JsonProperty("genre_ids")]
        public int[] GenreIds { get; set; }
        [JsonProperty("original_language")]
        public string OriginalLanguage { get; set; }
        [JsonProperty("release_date")]
        public DateTime ReleaseDate { get; set; }
        [JsonProperty("vote_count")]
        public int VoteCount { get; set; }
    }

}