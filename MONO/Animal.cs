using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MONO
{
    public abstract class Animal : IFoodCostCalculator
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Species { get; private set; }
        public Gender Gender { get; private set; }
        public int Age { get; private set; }
        public string FoodType { get; private set; }
        public double DailyFoodAmountKg { get; private set; }
        public double FoodPricePerKg { get; private set; }

        protected Animal(
            int id,
            string name,
            string species,
            Gender gender,
            int age,
            string foodType,
            double dailyFoodAmountKg,
            double foodPricePerKg)
        {
            Id = id;
            Name = name;
            Species = species;
            Gender = gender;
            Age = age;
            FoodType = foodType;
            DailyFoodAmountKg = dailyFoodAmountKg;
            FoodPricePerKg = foodPricePerKg;
        }

        public double CalculateDailyFoodCost()
        {
            return DailyFoodAmountKg * FoodPricePerKg;
        }

        public double CalculateMonthlyFoodCost()
        {
            return CalculateDailyFoodCost() * 30;
        }

        public abstract void MakeSound();

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Species: {Species}");
            Console.WriteLine($"Gender: {Gender}");
            Console.WriteLine($"Age: {Age}");
            Console.WriteLine($"Food type: {FoodType}");
            Console.WriteLine($"Daily food amount: {DailyFoodAmountKg} kg");
            Console.WriteLine($"Food price per kg: {FoodPricePerKg} EUR");
            Console.WriteLine($"Daily food cost: {CalculateDailyFoodCost()} EUR");
            Console.WriteLine($"Monthly food cost: {CalculateMonthlyFoodCost()} EUR");
        }
    }
}
