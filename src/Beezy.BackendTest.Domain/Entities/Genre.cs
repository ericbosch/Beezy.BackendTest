namespace Beezy.BackendTest.Domain.Entities
{
    public class Genre
    {
        public int Id { get; }
        public string Name { get; }

        private Genre()
        {

        }

        public Genre(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
