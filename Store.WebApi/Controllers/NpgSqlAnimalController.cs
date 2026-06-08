using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Store.Common;
using Store.Model;
using Store.Repository;
using Store.Repository.Common;
using Store.Service;
using Store.Service.Common;




namespace Store.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NpgSqlAnimalController : ControllerBase
    {
        private readonly NpgSqlAnimalService animalService = new NpgSqlAnimalService();

      

        [HttpGet]
        public IActionResult GetAllAnimals([FromQuery] AnimalFilter filter)
        {

            return Ok(animalService.GetAllAnimals(filter));
        }

        [HttpGet("{id}")]
        public IActionResult GetAnimalById(int id)
        {
            Animal animal = animalService.GetAnimalById(id);

            if (animal == null)
            {
                return NotFound("Animal with this ID does not exist.");
            }

            return Ok(animal);
        }

        [HttpPost]
        public IActionResult CreateAnimal(Animal animal)
        {
            Animal createdAnimal = animalService.CreateAnimal(animal);

            return Ok(createdAnimal);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAnimal(int id, Animal animal)
        {
            bool isUpdated = animalService.UpdateAnimal(id, animal);

            if (!isUpdated)
            {
                return NotFound("Animal with this ID does not exist.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAnimal(int id)
        {
            bool isDeleted = animalService.DeleteAnimal(id);

            if (!isDeleted)
            {
                return NotFound("Animal with this ID does not exist.");
            }

            return NoContent();
        }
    }
}
