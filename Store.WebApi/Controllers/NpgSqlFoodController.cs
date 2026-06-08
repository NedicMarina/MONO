using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Store.WebApi.Models;
using System.Text;
using Store.Model;
using Store.Service.Common;
using Store.Repository;
using Store.Service;
using Store.Common;

namespace Store.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class NpgSqlFoodController : ControllerBase
    {
        private readonly NpgSqlFoodService foodService = new NpgSqlFoodService();

    

        [HttpGet]
        public IActionResult GetAllFoods([FromQuery] FoodFilter filter)
        {
            List<Food> foods = foodService.GetAllFoods(filter);

            return Ok(foods);
        }

        [HttpGet("{id}")]
        public IActionResult GetFoodById(int id)
        {
            Food food = foodService.GetFoodById(id);

            if (food == null)
            {
                return NotFound("Food with this ID does not exist.");
            }

            return Ok(food);
        }

        [HttpPost]
        public IActionResult CreateFood(Food food)
        {
            Food createdFood = foodService.CreateFood(food);

            return Ok(createdFood);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateFood(int id, Food food)
        {
            bool isUpdated = foodService.UpdateFood(id, food);

            if (!isUpdated)
            {
                return NotFound("Food with this ID does not exist.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFood(int id)
        {
            bool isDeleted = foodService.DeleteFood(id);

            if (!isDeleted)
            {
                return NotFound("Food with this ID does not exist.");
            }

            return NoContent();
        }
    }
}