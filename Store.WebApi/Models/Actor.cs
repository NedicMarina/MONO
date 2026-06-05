namespace Store.WebApi.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }

        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfFirstWorkDay { get; set; }
        public string? Gender { get; set; }
        public List<ActorPerformance> ActorPerformances { get; set; }

    }
}
