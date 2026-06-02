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
                FoodId = 1
            },
            new Animal
            {
                Id = 2,
                Species = "Elephant",
                Name = "Dumbo",
                Age = 10,
                FoodId = 2
            }
        };

        [HttpGet]
        public IActionResult GetAllAnimals(
            string species="",
            string name = "",
            int minAge = 0)
        {
            IEnumerable<Animal> query = animals;

            query = query.Where(animal =>
                animal.Species.Contains(species));

            query = query.Where(animal =>
                animal.Name.Contains(name));

            query = query.Where(animal =>
                animal.Age >= minAge);

            return Ok(query.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetAnimalById(int id)
        {
            Animal animal = animals.FirstOrDefault(animal => animal.Id == id);

            if (animal == null)
            {
                return NotFound("Animal with this ID does not exist.");
            }

            return Ok(animal);
        }

        [HttpPost]
        public IActionResult CreateAnimal(Animal animal)
        {
            int newId = animals.Max(animal => animal.Id) + 1;

            animal.Id = newId;

            animals.Add(animal);

            return Ok("Animal added.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAnimal(int id, Animal updatedAnimal)
        {
            Animal animal = animals.FirstOrDefault(animal => animal.Id == id);

            if (animal == null)
            {
                return NotFound("Animal with this ID does not exist.");
            }

            animal.Species = updatedAnimal.Species;
            animal.Name = updatedAnimal.Name;
            animal.Age = updatedAnimal.Age;
            animal.FoodId = updatedAnimal.FoodId;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAnimal(int id)
        {
            Animal animal = animals.FirstOrDefault(animal => animal.Id == id);

            if (animal == null)
            {
                return NotFound("Animal with this ID does not exist.");
            }

            animals.Remove(animal);

            return NoContent();
        }
    }
}

