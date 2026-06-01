namespace MONO
{
    public class ZooAnimal : Animal
    {
        public DateTime AdmissionDate { get; private set; }
        public OriginType OriginType { get; private set; }

        public ZooAnimal(
            int id,
            string name,
            string species,
            Gender gender,
            int age,
            string foodType,
            double dailyFoodAmountKg,
            double foodPricePerKg,
            DateTime admissionDate,
            OriginType originType)
            : base(id, name, species, gender, age, foodType, dailyFoodAmountKg, foodPricePerKg)
        {
            AdmissionDate = admissionDate;
            OriginType = originType;
        }

        public override void MakeSound()
        {
            Console.WriteLine($"{Species} makes a sound.");
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Admission date: {AdmissionDate.ToShortDateString()}");
            Console.WriteLine($"Origin: {OriginType}");
        }
    }
}