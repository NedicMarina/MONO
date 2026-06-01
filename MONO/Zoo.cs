using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MONO
{
    public class Zoo
    {
        private List<Animal> animals = new List<Animal>();
        private List<RemovedAnimal> removedAnimals = new List<RemovedAnimal>();

        public void AddAnimal(Animal animal)
        {
            animals.Add(animal);
        }

        public void ShowAnimals()
        {
            if (animals.Count == 0)
            {
                Console.WriteLine("There are no animals in the zoo.");
                return;
            }

            foreach (Animal animal in animals)
            {
                animal.DisplayInfo();
                animal.MakeSound();
                Console.WriteLine("-------------------------");
            }
        }

        public void RemoveAnimal(int id, RemovalReason reason, string customReason)
        {
            Animal animalToRemove = animals.FirstOrDefault(animal => animal.Id == id);

            if (animalToRemove == null)
            {
                Console.WriteLine("Animal with this ID does not exist.");
                return;
            }

            animals.Remove(animalToRemove);

            RemovedAnimal removedAnimal = new RemovedAnimal(
                animalToRemove,
                reason,
                customReason);

            removedAnimals.Add(removedAnimal);

            Console.WriteLine("Animal removed successfully.");
        }

        public void ShowRemovedAnimals()
        {
            if (removedAnimals.Count == 0)
            {
                Console.WriteLine("There are no removed animals.");
                return;
            }

            foreach (RemovedAnimal removedAnimal in removedAnimals)
            {
                removedAnimal.DisplayInfo();
                Console.WriteLine("-------------------------");
            }
        }

        public double CalculateTotalDailyFoodCost()
        {
            double total = 0;

            foreach (Animal animal in animals)
            {
                total += animal.CalculateDailyFoodCost();
            }

            return total;
        }

        public double CalculateTotalMonthlyFoodCost()
        {
            return CalculateTotalDailyFoodCost() * 30;
        }

        public void ShowMostExpensiveAnimal()
        {
            if (animals.Count == 0)
            {
                Console.WriteLine("There are no animals in the zoo.");
                return;
            }

            Animal mostExpensiveAnimal = animals[0];

            foreach (Animal animal in animals)
            {
                if (animal.CalculateDailyFoodCost() > mostExpensiveAnimal.CalculateDailyFoodCost())
                {
                    mostExpensiveAnimal = animal;
                }
            }

            Console.WriteLine("Most expensive animal for feeding:");
            mostExpensiveAnimal.DisplayInfo();
        }
    }
}
