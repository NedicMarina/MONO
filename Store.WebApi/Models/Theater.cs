namespace Store.WebApi.Models
{
    public class Theater
    {
        public int Id { get; set; }
        public string? Adress { get; set; }
        public string? Title { get; set; }
        public string? WorkingHours { get; set; }

        public List<Seat> Seats { get; set; }
        public List<Performance> Performances { get; set; }
    }
}
