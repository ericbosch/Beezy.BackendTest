using System;

namespace Beezy.BackendTest.Domain.Entities
{
    public class Session
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int MovieId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int? SeatsSold { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual Room Room { get; set; }

        public Session()
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
