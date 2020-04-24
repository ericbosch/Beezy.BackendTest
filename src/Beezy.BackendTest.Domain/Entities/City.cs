using System.Collections.Generic;

namespace Beezy.BackendTest.Domain.Entities
{
    public class City
    {

        public int Id { get; }
        public string Name { get; }
        public int Population { get; }

        public virtual ICollection<Cinema> Cinema { get; }

        private City()
        {
            Cinema = new HashSet<Cinema>();
        }

        public City(int id, string cityName, int cityPopulation)
        {
            Id = id;
            Name = cityName;
            Population = cityPopulation;
            Cinema = new HashSet<Cinema>();
        }
    }
}
