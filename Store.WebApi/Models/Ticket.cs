namespace Store.WebApi.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public decimal Price { get; set; }


        public int AudienceMemberId { get; set; }
        public AudienceMember AudienceMember { get; set; }

        public int SeatId { get; set; }
        public Seat Seat { get; set; }

        public int PerformanceId { get; set; }
        public Performance Performance { get; set; }
    }
}
