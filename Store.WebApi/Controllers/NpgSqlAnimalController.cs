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
        public async Task<IActionResult> GetAllAnimalsAsync([FromQuery] AnimalFilter filter)
        {
            return Ok(await animalService.GetAllAnimalsAsync(filter));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnimalByIdAsync(int id)
        {
            Animal animal = await animalService.GetAnimalByIdAsync(id);

            if (animal == null)
            {
                return NotFound("Animal with this ID does not exist.");
            }

            return Ok(animal);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnimalAsync(Animal animal)
        {
            Animal createdAnimal = await animalService.CreateAnimalAsync(animal);

            return Ok(createdAnimal);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnimalAsync(int id, Animal animal)
        {
            bool isUpdated = await animalService.UpdateAnimalAsync(id, animal);

            if (!isUpdated)
            {
                return NotFound("Animal with this ID does not exist.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimalAsync(int id)
        {
            bool isDeleted = await animalService.DeleteAnimalAsync(id);

            if (!isDeleted)
            {
                return NotFound("Animal with this ID does not exist.");
            }

            return NoContent();
        }
    }
}
