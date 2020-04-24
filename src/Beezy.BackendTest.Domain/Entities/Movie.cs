using System;
using System.Collections.Generic;

namespace Beezy.BackendTest.Domain.Entities
{
    public class Movie
    {

        public int Id { get; set; }
        public string OriginalTitle { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string OriginalLanguage { get; set; }
        public bool Adult { get; set; }

        public virtual ICollection<Session> Session { get; }

        public Movie()
        {
            Session = new HashSet<Session>();
        }

        public Movie(int id, string originalTitle, DateTime releaseDate, string originalLanguage, bool adult)
        {
            Id = id;
            OriginalTitle = originalTitle;
            ReleaseDate = releaseDate;
            OriginalLanguage = originalLanguage;
            Adult = adult;
            Session = new HashSet<Session>();
        }
    }
}
