using Microsoft.AspNetCore.Mvc;
using Store.WebApi.Models;


namespace Store.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class FoodController : ControllerBase
    {
        private static List<Food> foods = new List<Food>
        {
            new Food
            {
                Id = 1,
                Name = "Meat",
                PricePerKg = 6.5,
                QuantityInStockKg = 100
            },
            new Food
            {
                Id = 2,
                Name = "Grass",
                PricePerKg = 1.2,
                QuantityInStockKg = 500
            },
            new Food
            {
                Id = 3,
                Name = "Mouse",
                PricePerKg = 4,
                QuantityInStockKg = 30
            }
        };

        [HttpGet]
        public IActionResult GetFoods(
            string name = "",
            double maxPrice = 0,
            double minQuantity = 0)
        {
            IEnumerable<Food> query = foods;

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(food => food.Name.ToLower().Contains(name.ToLower()));
            }

            if (maxPrice > 0)
            {
                query = query.Where(food => food.PricePerKg <= maxPrice);
            }

            if (minQuantity > 0)
            {
                query = query.Where(food => food.QuantityInStockKg >= minQuantity);
            }

            return Ok(query.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetFoodById(int id)
        {
            Food food = foods.FirstOrDefault(food => food.Id == id);

            if (food == null)
            {
                return NotFound("Food with this ID does not exist.");
            }

            return Ok(food);
        }

        [HttpPost]
        public IActionResult CreateFood(Food food)
        {
            int newId = foods.Max(food => food.Id) + 1;

            food.Id = newId;

            foods.Add(food);

            return Ok(food);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateFood(int id, Food updatedFood)
        {
            Food food = foods.FirstOrDefault(food => food.Id == id);

            if (food == null)
            {
                return NotFound("Food with this ID does not exist.");
            }

            food.Name = updatedFood.Name;
            food.PricePerKg = updatedFood.PricePerKg;
            food.QuantityInStockKg = updatedFood.QuantityInStockKg;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFood(int id)
        {
            Food food = foods.FirstOrDefault(food => food.Id == id);

            if (food == null)
            {
                return NotFound("Food with this ID does not exist.");
            }

            foods.Remove(food);

            return NoContent();
        }
    }
}
