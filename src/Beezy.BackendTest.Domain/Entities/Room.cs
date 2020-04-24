using System.Collections.Generic;

namespace Beezy.BackendTest.Domain.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public int Seats { get; set; }
        public int CinemaId { get; set; }

        public virtual Cinema Cinema { get; }
        public virtual ICollection<Session> Session { get; }

        public Room()
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
