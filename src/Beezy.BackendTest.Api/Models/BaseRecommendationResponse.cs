using System;
using System.Collections.Generic;

namespace Beezy.BackendTest.Api.Models
{
    public abstract class BaseRecommendationResponse
    {
        public string Title { get; set; }
        public string Overview { get; set; }
        public List<string> Genres { get; set; }
        public string Language { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string WebSite { get; set; }
        public List<string> Keywords { get; set; }
    }
}