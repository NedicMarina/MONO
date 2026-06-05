using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Store.WebApi.Models;
using System.Text;

namespace Store.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class NpgSqlFoodController : ControllerBase
    {
        private readonly string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres";

      
        [HttpGet]
        public IActionResult GetAllFoods(string name = "", double maxPrice = 0, double minQuantity = 0)
        {
            List<Food> foods = new List<Food>();

            NpgsqlConnection connection =  new NpgsqlConnection(connectionString);

            connection.Open();

            StringBuilder queryBuilder = new StringBuilder();

            queryBuilder.Append(
                "SELECT \"Id\", \"Name\", \"PricePerKg\", \"QuantityInStockKg\" " + "FROM \"Food\" WHERE 1 = 1 ");

            NpgsqlCommand command = new NpgsqlCommand();

            command.Connection = connection;

            if (!string.IsNullOrWhiteSpace(name))
            {
                queryBuilder.Append("AND \"Name\" LIKE @name ");

                command.Parameters.AddWithValue("name", name);
            }

            if (maxPrice > 0)
            {
                queryBuilder.Append("AND \"PricePerKg\" <= @maxPrice ");

                command.Parameters.AddWithValue("maxPrice", maxPrice);
            }

            if (minQuantity > 0)
            {
                queryBuilder.Append("AND \"QuantityInStockKg\" >= @minQuantity ");

                command.Parameters.AddWithValue("minQuantity", minQuantity);
            }

            command.CommandText = queryBuilder.ToString();

            NpgsqlDataReader reader = command.ExecuteReader();


            while (reader.Read())
            {
                Food food = new Food
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    PricePerKg = Convert.ToDouble(reader.GetValue(2)),
                    QuantityInStockKg = Convert.ToDouble(reader.GetValue(3))
                };

                foods.Add(food);
            }

            connection.Close();

            return Ok(foods);
        }

        [HttpGet("{id}")]
        public IActionResult GetFoodById(int id)
        {
            Food food = null;

            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();

            string commandText = "SELECT \"Id\", \"Name\", \"PricePerKg\", \"QuantityInStockKg\" FROM \"Food\" WHERE \"Id\" = @id";

            NpgsqlCommand command = new NpgsqlCommand(commandText, connection);
            command.Parameters.AddWithValue("id", id);

            NpgsqlDataReader reader = command.ExecuteReader();

            if (reader.Read()){

                food = new Food
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    PricePerKg = Convert.ToDouble(reader.GetValue(2)),
                    QuantityInStockKg = Convert.ToDouble(reader.GetValue(3))
                };
            }

            connection.Close();

            if (food == null)
            {
                return NotFound("Food with this ID does not exist.");
            }

            return Ok(food);
        }

        [HttpPost]
        public IActionResult CreateFood(Food food)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();

            string maxIdQuery = "SELECT COALESCE(MAX(\"Id\"), 0) FROM \"Food\"";

            NpgsqlCommand maxIdCommand = new NpgsqlCommand(maxIdQuery, connection);

            int newId = Convert.ToInt32(maxIdCommand.ExecuteScalar()) + 1;

            string commandText =
                "INSERT INTO \"Food\" (\"Id\", \"Name\", \"PricePerKg\", \"QuantityInStockKg\") " +
                "VALUES (@id, @name, @pricePerKg, @quantityInStockKg)";

            NpgsqlCommand command = new NpgsqlCommand(commandText, connection);

            command.Parameters.AddWithValue("id", newId);
            command.Parameters.AddWithValue("name", food.Name);
            command.Parameters.AddWithValue("pricePerKg", food.PricePerKg);
            command.Parameters.AddWithValue("quantityInStockKg", food.QuantityInStockKg);

            int affectedRows = command.ExecuteNonQuery();

            connection.Close();

            if (affectedRows == 0)
            {
                return BadRequest("Food was not created.");
            }

            food.Id = newId;

            return Ok(food);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateFood(int id, Food updatedFood)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();

            string commandText =
                "UPDATE \"Food\" " +
                "SET \"Name\" = @name, \"PricePerKg\" = @pricePerKg, \"QuantityInStockKg\" = @quantityInStockKg " +
                "WHERE \"Id\" = @id";

            NpgsqlCommand command = new NpgsqlCommand(commandText, connection);

            command.Parameters.AddWithValue("id", id);
            command.Parameters.AddWithValue("name", updatedFood.Name);
            command.Parameters.AddWithValue("pricePerKg", updatedFood.PricePerKg);
            command.Parameters.AddWithValue("quantityInStockKg", updatedFood.QuantityInStockKg);

            int affectedRows = command.ExecuteNonQuery();

            connection.Close();

            if (affectedRows == 0)
            {
                return NotFound("Food with this ID does not exist.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFood(int id)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();

            string commandText =
                "DELETE FROM \"Food\" WHERE \"Id\" = @id";

            NpgsqlCommand command = new NpgsqlCommand(commandText, connection);
            command.Parameters.AddWithValue("id", id);

            int affectedRows = command.ExecuteNonQuery();

            connection.Close();

            if (affectedRows == 0)
            {
                return NotFound("Food with this ID does not exist.");
            }

            return NoContent();
        }
    }
}

