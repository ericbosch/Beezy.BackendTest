using System;

namespace Beezy.BackendTest.Domain.Entities
{
    public class Session
    {
        public int Id { get; }
        public int RoomId { get; }
        public int MovieId { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public int? SeatsSold { get; private set; }

        public virtual Movie Movie { get; }
        public virtual Room Room { get; }

        private Session()
        {

        }

        public Session(int id, int roomId, int movieId, DateTime startTime, DateTime endTime, int? seatsSold, Movie movie, Room room)
        {
            Id = id;
            RoomId = roomId;
            MovieId = movieId;
            StartTime = startTime;
            EndTime = endTime;
            SeatsSold = seatsSold;
            Movie = movie;
            Room = room;
        }
    }
}
