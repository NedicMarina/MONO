namespace Store.Model
{
    public class Animal
    {
        public int Id { get; set; }

        public string Species { get; set; }

        public string Name { get; set; } = "NoName";

        public int Age { get; set; } = 0;

        public int FoodId { get; set; }
    }
}
