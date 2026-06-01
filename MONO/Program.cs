using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MONO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Zoo zoo = new Zoo();

            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine();
                Console.WriteLine("ZOO FOOD COST CALCULATOR");
                Console.WriteLine("1. Add animal");
                Console.WriteLine("2. Show animals");
                Console.WriteLine("3. Show total daily food cost");
                Console.WriteLine("4. Show total monthly food cost");
                Console.WriteLine("5. Show most expensive animal");
                Console.WriteLine("6. Remove animal");
                Console.WriteLine("7. Show removed animals");
                Console.WriteLine("8. Exit");
                Console.Write("Choose option: ");

                int option = int.Parse(Console.ReadLine());

                Console.WriteLine();

                switch (option)
                {
                    case 1:
                        AddAnimal(zoo);
                        break;

                    case 2:
                        zoo.ShowAnimals();
                        break;

                    case 3:
                        Console.WriteLine($"Total daily food cost: {zoo.CalculateTotalDailyFoodCost()} EUR");
                        break;

                    case 4:
                        Console.WriteLine($"Total monthly food cost: {zoo.CalculateTotalMonthlyFoodCost()} EUR");
                        break;

                    case 5:
                        zoo.ShowMostExpensiveAnimal();
                        break;

                    case 6:
                        RemoveAnimal(zoo);
                        break;

                    case 7:
                        zoo.ShowRemovedAnimals();
                        break;

                    case 8:
                        isRunning = false;
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        static void AddAnimal(Zoo zoo)
        {
            Console.Write("Enter ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Enter name: ");
            string name = Console.ReadLine();

            Console.Write("Enter species: ");
            string species = Console.ReadLine();

            Console.WriteLine("Choose gender:");
            Console.WriteLine("1. Male");
            Console.WriteLine("2. Female");
            int genderInput = int.Parse(Console.ReadLine());
            Gender gender = (Gender)genderInput;

            Console.Write("Enter age: ");
            int age = int.Parse(Console.ReadLine());

            Console.Write("Enter food type: ");
            string foodType = Console.ReadLine();

            Console.Write("Enter daily food amount in kg: ");
            double dailyFoodAmountKg = double.Parse(Console.ReadLine());

            Console.Write("Enter food price per kg: ");
            double foodPricePerKg = double.Parse(Console.ReadLine());

            Console.Write("Enter admission date (yyyy-mm-dd): ");
            DateTime admissionDate = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Choose origin:");
            Console.WriteLine("1. Born in zoo");
            Console.WriteLine("2. From wild");
            Console.WriteLine("3. From another zoo");
            int originInput = int.Parse(Console.ReadLine());
            OriginType originType = (OriginType)originInput;

            Animal animal = new ZooAnimal(
                id,
                name,
                species,
                gender,
                age,
                foodType,
                dailyFoodAmountKg,
                foodPricePerKg,
                admissionDate,
                originType);

            zoo.AddAnimal(animal);

            Console.WriteLine("Animal added successfully.");
        }

        static void RemoveAnimal(Zoo zoo)
        {
            Console.Write("Enter animal ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.WriteLine("Choose removal reason:");
            Console.WriteLine("1. Died");
            Console.WriteLine("2. Donated");
            Console.WriteLine("3. Other");

            int reasonInput = int.Parse(Console.ReadLine());
            RemovalReason reason = (RemovalReason)reasonInput;

            string customReason = "";

            if (reason == RemovalReason.Other)
            {
                Console.Write("Enter custom reason: ");
                customReason = Console.ReadLine();
            }

            zoo.RemoveAnimal(id, reason, customReason);
        }
    }
}