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
        public async Task<IActionResult> GetAllFoodsAsync([FromQuery] FoodFilter filter)
        {
            List<Food> foods = await foodService.GetAllFoodsAsync(filter);

            return Ok(foods);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFoodByIdAsync(int id)
        {
            Food food = await foodService.GetFoodByIdAsync(id);

            if (food == null)
            {
                return NotFound("Food with this ID does not exist.");
            }

            return Ok(food);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFoodAsync(Food food)
        {
            Food createdFood = await foodService.CreateFoodAsync(food);

            return Ok(createdFood);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFoodAsync(int id, Food food)
        {
            bool isUpdated = await foodService.UpdateFoodAsync(id, food);

            if (!isUpdated)
            {
                return NotFound("Food with this ID does not exist.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFoodAsync(int id)
        {
            bool isDeleted = await foodService.DeleteFoodAsync(id);

            if (!isDeleted)
            {
                return NotFound("Food with this ID does not exist.");
            }

            return NoContent();
        }
    }
}