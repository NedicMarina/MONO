namespace Store.WebApi.Models
{
    public class Seat   
    {
        public int Id { get; set; }
        public int No { get; set; }
        public string? Category { get; set; }
        public int TheaterId { get; set; }
        public Theater Theater { get; set; }

        public List<Ticket> Tickets { get; set; } = new();
    }
}
