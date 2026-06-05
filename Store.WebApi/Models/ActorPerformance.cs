namespace Store.WebApi.Models
{
    public class ActorPerformance
    {
        public int Id { get; set; }

        public int ActorId { get; set; }
        public Actor Actor { get; set; }

        public int PerformanceId { get; set; }
        public Performance Performance { get; set; }
    }
}
