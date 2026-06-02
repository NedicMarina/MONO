using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MONO
{
    public class RemovedAnimal : Animal
    {
        public RemovalReason Reason { get; private set; }
        public string CustomReason { get; private set; }
        public DateTime RemovalDate { get; private set; }

        public RemovedAnimal(
            Animal animal,
            RemovalReason reason,
            string customReason)
            : base(
                animal.Id,
                animal.Name,
                animal.Species,
                animal.Gender,
                animal.Age,
                animal.FoodType,
                animal.DailyFoodAmountKg,
                animal.FoodPricePerKg)
        {
            Reason = reason;
            CustomReason = customReason;
            RemovalDate = DateTime.Now;
        }

        public override void MakeSound()
        {
            Console.WriteLine($"{Species} is no longer in the zoo.");
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Removal reason: {Reason}");

            if (Reason == RemovalReason.Other)
            {
                Console.WriteLine($"Custom reason: {CustomReason}");
            }

            Console.WriteLine($"Removal date: {RemovalDate}");
        }
    }
}