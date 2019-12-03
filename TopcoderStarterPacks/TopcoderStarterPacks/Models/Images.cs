using Models.Interfaces;

namespace Models
{
    public class Images : IModel
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public int Version { get; set; }

    }
}
