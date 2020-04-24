using System;
using System.Collections.Generic;

namespace Beezy.BackendTest.Domain.Entities
{
    public partial class Cinema
    {
        public int Id { get; }
        public string Name { get; }
        public DateTime OpenSince { get; }
        public int CityId { get; }

        public virtual City City { get; }
        public virtual ICollection<Room> Room { get; }

        private Cinema()
        {
            Room = new HashSet<Room>();
        }

        public Cinema(int id, string name, DateTime openSince, City city, int cityId)
        {
            Id = id;
            Name = name;
            OpenSince = openSince;
            City = city;
            CityId = cityId;
            Room = new HashSet<Room>();
        }
    }
}
