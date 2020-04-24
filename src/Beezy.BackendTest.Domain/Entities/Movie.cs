using System;
using System.Collections.Generic;

namespace Beezy.BackendTest.Domain.Entities
{
    public class Movie
    {

        public int Id { get; }
        public string OriginalTitle { get; }
        public DateTime ReleaseDate { get; }
        public string OriginalLanguage { get; }
        public bool Adult { get; }

        public virtual ICollection<Session> Session { get; }

        private Movie()
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
