using System;
using System.Collections.Generic;

namespace Beezy.BackendTest.Domain.Entities
{
    public partial class Cinema
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime OpenSince { get; set; }
        public int CityId { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<Room> Room { get; set; }

        public Cinema()
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
