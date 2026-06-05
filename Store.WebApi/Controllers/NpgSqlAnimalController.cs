using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Store.WebApi.Models;
using System.Text;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;


namespace Store.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NpgSqlAnimalController : ControllerBase
    {
        private readonly string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres";

        [HttpGet]
        public IActionResult GetAllAnimals(string species = "", string name = "", int minAge = 0)
        {
            List<Animal> animals = new List<Animal>();

            NpgsqlConnection conn = new NpgsqlConnection(connectionString);

            conn.Open();

            StringBuilder queryBuilder = new StringBuilder();

            queryBuilder.Append( "SELECT \"Id\", \"Species\", \"Name\", \"Age\", \"FoodId\" " + "FROM \"Animal\" WHERE 1 = 1 ");

            NpgsqlCommand command = new NpgsqlCommand();

            command.Connection = conn;

            if (!string.IsNullOrWhiteSpace(species))
            {
                queryBuilder.Append(
                    "AND \"Species\" = @species ");

                command.Parameters.AddWithValue("species", species);
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                queryBuilder.Append("AND \"Name\" = @name ");

                command.Parameters.AddWithValue("name",name);
            }

            if (minAge > 0)
            {
                queryBuilder.Append("AND \"Age\" >= @minAge ");

                command.Parameters.AddWithValue("minAge", minAge);
            }

            command.CommandText = queryBuilder.ToString();

            NpgsqlDataReader reader =  command.ExecuteReader();

            while (reader.Read())
            {
                Animal animal = new Animal
                {
                    Id = reader.GetInt32(0),
                    Species = reader.GetString(1),
                    Name = reader.GetString(2),
                    Age = reader.GetInt32(3),
                    FoodId = reader.GetInt32(4)
                };

                animals.Add(animal);
            }

            conn.Close();

            return Ok(animals);
        }

        [HttpGet("{id}")]
        public IActionResult GetAnimalById(int id)
        {
            Animal animal = null;

            NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            conn.Open();

            string commandText = "SELECT \"Id\", \"Species\", \"Name\", \"Age\", \"FoodId\" FROM \"Animal\" WHERE \"Id\" = @id";
            NpgsqlCommand command = new NpgsqlCommand(commandText, conn);
            command.Parameters.AddWithValue("id", id);

            NpgsqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {

                animal = new Animal
                {
                    Id = reader.GetInt32(0),
                    Species = reader.GetString(1),
                    Name = reader.GetString(2),
                    Age = reader.GetInt32(3),
                    FoodId = reader.GetInt32(4)
                };
            }

            conn.Close();

            if (animal == null)
            {
                return NotFound("Animal with this ID does not exist.");
            }

            return Ok(animal);
        }

        [HttpPost]
        public IActionResult CreateAnimal(Animal animal)
        {
            NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            conn.Open();

            string maxIdQuery = "SELECT COALESCE(MAX(\"Id\"), 0) FROM \"Animal\"";

            NpgsqlCommand maxIdCommand = new NpgsqlCommand(maxIdQuery, conn);
            int newId = Convert.ToInt32(maxIdCommand.ExecuteScalar()) + 1;

            string commandText = "INSERT INTO \"Animal\" (\"Id\", \"Species\", \"Name\", \"Age\", \"FoodId\")" + "VALUES (@id, @species, @name, @age, @foodId)";
            NpgsqlCommand command = new NpgsqlCommand(commandText, conn);

            command.Parameters.AddWithValue("id", newId);
            command.Parameters.AddWithValue("species", animal.Species);
            command.Parameters.AddWithValue("name", animal.Name);
            command.Parameters.AddWithValue("age", animal.Age);
            command.Parameters.AddWithValue("foodId", animal.FoodId);

            int affectedRows = command.ExecuteNonQuery();

            conn.Close();
            if (affectedRows == 0)
            {
                return BadRequest("Animal was not created.");
            }

            animal.Id = newId;
            return Ok(animal);
        }
        
        [HttpPut("{id}")]
        public IActionResult UpdateAnimal(int id, Animal updatedAnimal)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();

            string commandText = "UPDATE \"Aniaml\"" +
                "SET \"Species\" = @species, \"Name\" = @name, \"Age\" = @age, \"FoodId\" = @foodId " +
                "WHERE \"Id\" = @id";
            NpgsqlCommand command = new NpgsqlCommand(commandText, connection);
           
            command.Parameters.AddWithValue("id", id);
            command.Parameters.AddWithValue("species", updatedAnimal.Species);
            command.Parameters.AddWithValue("name", updatedAnimal.Name);
            command.Parameters.AddWithValue("age", updatedAnimal.Age);
            command.Parameters.AddWithValue("foodId", updatedAnimal.FoodId);
           
            int affectedRows = command.ExecuteNonQuery();

            connection.Close();

            if (affectedRows == 0)
            {
                return NotFound("Animal with this ID does not exist.");
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteAnimal(int id)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();

            string commandText =
                "DELETE FROM \"Animal\" WHERE \"Id\" = @id";

            NpgsqlCommand command = new NpgsqlCommand(commandText, connection);
            command.Parameters.AddWithValue("id", id);

            int affectedRows = command.ExecuteNonQuery();

            connection.Close();

            if (affectedRows == 0)
            {
                return NotFound("Animal with this ID does not exist.");
            }

            return NoContent();
        }
    }
}

