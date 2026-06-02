using Microsoft.AspNetCore.Mvc;
using Store.WebApi.Models;


namespace Store.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimalsController : ControllerBase
    {
        private static List<Animal> animals = new List<Animal>
        {
            new Animal
            {
                Id = 1,
                Species = "Lion",
                Name = "Simba",
                Age = 5,
                FoodType = "Meat"
            },
            new Animal
            {
                Id = 2,
                Species = "Elephant",
                Name = "Dumbo",
                Age = 10,
                FoodType = "Grass"
            }
        };

        [HttpGet(Name = "GetAnimal")]
        public IEnumerable<Animal> GetAnimals()
        {


            return animals;
        }

        [HttpGet("{id}")]
        public Animal GetAnimalById(int id)
        {
            Animal animal = animals.FirstOrDefault(animal => animal.Id == id);

            return animal;
        }


        [HttpPost]
        public Animal CreateAnimal(Animal animal)
        {
            int newId = animals.Max(animal => animal.Id) + 1;

            animal.Id = newId;

            animals.Add(animal);

            return animal;
        }

        [HttpPut("{id}")]
        public Animal UpdateAnimal(int id, Animal updatedAnimal)
        {
            Animal animal = animals.FirstOrDefault(animal => animal.Id == id);

            if (animal == null)
            {
                return null;
            }

            animal.Species = updatedAnimal.Species;
            animal.Name = updatedAnimal.Name;
            animal.Age = updatedAnimal.Age;
            animal.FoodType = updatedAnimal.FoodType;

            return animal;
        }

        [HttpDelete("{id}")]
        public Animal DeleteAnimal(int id)
        {
            Animal animal = animals.FirstOrDefault(animal => animal.Id == id);

            if (animal == null)
            {
                return null;
            }

            animals.Remove(animal);

            return animal;
        }
    }
}
