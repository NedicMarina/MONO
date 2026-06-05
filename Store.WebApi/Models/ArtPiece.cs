namespace Store.WebApi.Models
{
    public class ArtPiece
    {
        public int Id { get; set; }
        public string? Genre { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public List<Performance>? Performances { get; set; }

    }
}
