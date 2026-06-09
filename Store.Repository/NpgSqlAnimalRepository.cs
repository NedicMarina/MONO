using Npgsql;
using Store.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Common;
using Store.Repository.Common;

namespace Store.Repository
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly string connectionString =
            "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres";

        public async Task<List<Animal>> GetAllAnimalsAsync(AnimalFilter filter)
        {
            List<Animal> animals = new List<Animal>();

            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();

            StringBuilder queryBuilder = new StringBuilder();

            queryBuilder.Append(
                "SELECT \"Id\", \"Species\", \"Name\", \"Age\", \"FoodId\" " + "FROM \"Animal\" WHERE 1 = 1 ");

            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;

            if (!string.IsNullOrWhiteSpace(filter.Species))
            {
                queryBuilder.Append("AND \"Species\" = @species ");
                command.Parameters.AddWithValue("species", filter.Species);
            }

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                queryBuilder.Append("AND \"Name\" = @name ");
                command.Parameters.AddWithValue("name", filter.Name);
            }

            if (filter.MinAge > 0)
            {
                queryBuilder.Append("AND \"Age\" >= @minAge ");
                command.Parameters.AddWithValue("minAge", filter.MinAge);
            }

            command.CommandText = queryBuilder.ToString();

            NpgsqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
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

            connection.Close();

            return animals;
        }

        public async Task<Animal> GetAnimalByIdAsync(int id)
        {
            Animal animal = null;

            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();

            string commandText =
                "SELECT \"Id\", \"Species\", \"Name\", \"Age\", \"FoodId\" " + "FROM \"Animal\" WHERE \"Id\" = @id";

            NpgsqlCommand command = new NpgsqlCommand(commandText, connection);
            command.Parameters.AddWithValue("id", id);

            NpgsqlDataReader reader =await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
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

            connection.Close();

            return animal;
        }

        public async Task<Animal> CreateAnimalAsync(Animal animal)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();

            string maxIdQuery = "SELECT COALESCE(MAX(\"Id\"), 0) FROM \"Animal\"";
            NpgsqlCommand maxIdCommand = new NpgsqlCommand(maxIdQuery, connection);

            int newId = Convert.ToInt32(await maxIdCommand.ExecuteScalarAsync()) + 1;

            string commandText =
                "INSERT INTO \"Animal\" (\"Id\", \"Species\", \"Name\", \"Age\", \"FoodId\") " +
                "VALUES (@id, @species, @name, @age, @foodId)";

            NpgsqlCommand command = new NpgsqlCommand(commandText, connection);

            command.Parameters.AddWithValue("id", newId);
            command.Parameters.AddWithValue("species", animal.Species);
            command.Parameters.AddWithValue("name", animal.Name);
            command.Parameters.AddWithValue("age", animal.Age);
            command.Parameters.AddWithValue("foodId", animal.FoodId);

            await command.ExecuteNonQueryAsync();

            connection.Close();

            animal.Id = newId;

            return animal;
        }

        public async Task<bool> UpdateAnimalAsync(int id, Animal animal)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();

            string commandText =
                "UPDATE \"Animal\" " +
                "SET \"Species\" = @species, \"Name\" = @name, \"Age\" = @age, \"FoodId\" = @foodId " +
                "WHERE \"Id\" = @id";

            NpgsqlCommand command = new NpgsqlCommand(commandText, connection);

            command.Parameters.AddWithValue("id", id);
            command.Parameters.AddWithValue("species", animal.Species);
            command.Parameters.AddWithValue("name", animal.Name);
            command.Parameters.AddWithValue("age", animal.Age);
            command.Parameters.AddWithValue("foodId", animal.FoodId);

            int affectedRows = await command.ExecuteNonQueryAsync();

            connection.Close();

            return affectedRows > 0;
        }

        public async Task<bool> DeleteAnimalAsync(int id)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();

            string commandText =
                "DELETE FROM \"Animal\" WHERE \"Id\" = @id";

            NpgsqlCommand command = new NpgsqlCommand(commandText, connection);
            command.Parameters.AddWithValue("id", id);

            int affectedRows = await command.ExecuteNonQueryAsync();

            connection.Close();

            return affectedRows > 0;
        }
    }
}
