namespace Store.WebApi.Models
{
    public class Performance
    {
        public int Id { get; set; }
        public DateTime DateOfPerformance { get; set; }

        public int? TheaterId { get; set; }
        public Theater Theater { get; set; }

        public int? ArtPieceId { get; set; }
        public ArtPiece ArtPiece { get; set; }

        public List<Ticket> Tickets { get; set; } = new();
        public List<ActorPerformance> ActorPerformances { get; set; } = new();
    }
}

