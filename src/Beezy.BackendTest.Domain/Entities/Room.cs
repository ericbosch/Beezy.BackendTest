using System.Collections.Generic;

namespace Beezy.BackendTest.Domain.Entities
{
    public class Room
    {
        public int Id { get; }
        public string Name { get; }
        public string Size { get; }
        public int Seats { get; }
        public int CinemaId { get; }

        public virtual Cinema Cinema { get; }
        public virtual ICollection<Session> Session { get; }

        private Room()
        {
            Session = new HashSet<Session>();
        }

        public Room(int id, string name, string size, int seats, int cinemaId, Cinema cinema)
        {
            Id = id;
            Name = name;
            Size = size;
            Seats = seats;
            CinemaId = cinemaId;
            Cinema = cinema;
            Session = new HashSet<Session>();
        }
    }
}
