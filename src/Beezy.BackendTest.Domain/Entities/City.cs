using System.Collections.Generic;

namespace Beezy.BackendTest.Domain.Entities
{
    public class City
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }

        public virtual ICollection<Cinema> Cinema { get; }

        public City()
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
