namespace Store.WebApi.Models
{
    public class AudienceMember
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }

        public List<Ticket> Tickets { get; set; }
    }
}
