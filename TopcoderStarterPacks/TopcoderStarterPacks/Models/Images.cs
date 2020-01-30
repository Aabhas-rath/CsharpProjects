using Models.Interfaces;

namespace Models
{
    public class Image : IModel
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public int Version { get; set; }
        public int? AlbumId { get; set; }
        public Album Album { get; set; }
    }
}
