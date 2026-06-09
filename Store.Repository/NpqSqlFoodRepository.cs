using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;
using Store.Repository.Common;  
using Store.Model;
using Store.Common;

namespace Store.Repository
{
    public class NpgSqlFoodRepository : INpgSqlFoodRepository
    {
        private readonly string connectionString =
            "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres";

        public async Task<List<Food>> GetAllFoodsAsync(FoodFilter filter)
        {
            List<Food> foods = new List<Food>();

            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();

            StringBuilder queryBuilder = new StringBuilder();

            queryBuilder.Append(
                "SELECT \"Id\", \"Name\", \"PricePerKg\", \"QuantityInStockKg\" " +
                "FROM \"Food\" WHERE 1 = 1 ");

            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                queryBuilder.Append("AND \"Name\" = @name ");
                command.Parameters.AddWithValue("name", filter.Name);
            }

            if (filter.MaxPrice > 0)
            {
                queryBuilder.Append("AND \"PricePerKg\" <= @maxPrice ");
                command.Parameters.AddWithValue("maxPrice", filter.MaxPrice);
            }

            if (filter.MinQuantity > 0)
            {
                queryBuilder.Append("AND \"QuantityInStockKg\" >= @minQuantity ");
                command.Parameters.AddWithValue("minQuantity", filter.MinQuantity);
            }

            command.CommandText = queryBuilder.ToString();

            NpgsqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
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

            return foods;
        }

        public async Task<Food> GetFoodByIdAsync(int id)
        {
            Food food = null;

            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();

            string commandText =
                "SELECT \"Id\", \"Name\", \"PricePerKg\", \"QuantityInStockKg\" " +
                "FROM \"Food\" WHERE \"Id\" = @id";

            NpgsqlCommand command = new NpgsqlCommand(commandText, connection);
            command.Parameters.AddWithValue("id", id);

            NpgsqlDataReader reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                food = new Food
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    PricePerKg = Convert.ToDouble(reader.GetValue(2)),
                    QuantityInStockKg = Convert.ToDouble(reader.GetValue(3))
                };
            }

            connection.Close();

            return food;
        }

        public async Task<Food> CreateFoodAsync(Food food)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();

            string maxIdQuery = "SELECT COALESCE(MAX(\"Id\"), 0) FROM \"Food\"";

            NpgsqlCommand maxIdCommand = new NpgsqlCommand(maxIdQuery, connection);

            int newId = Convert.ToInt32(await maxIdCommand.ExecuteScalarAsync()) + 1;

            string commandText =
                "INSERT INTO \"Food\" (\"Id\", \"Name\", \"PricePerKg\", \"QuantityInStockKg\") " +
                "VALUES (@id, @name, @pricePerKg, @quantityInStockKg)";

            NpgsqlCommand command = new NpgsqlCommand(commandText, connection);

            command.Parameters.AddWithValue("id", newId);
            command.Parameters.AddWithValue("name", food.Name);
            command.Parameters.AddWithValue("pricePerKg", food.PricePerKg);
            command.Parameters.AddWithValue("quantityInStockKg", food.QuantityInStockKg);

            await command.ExecuteNonQueryAsync();

            connection.Close();

            food.Id = newId;

            return food;
        }

        public async Task<bool> UpdateFoodAsync(int id, Food food)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();

            string commandText =
                "UPDATE \"Food\" " +
                "SET \"Name\" = @name, \"PricePerKg\" = @pricePerKg, \"QuantityInStockKg\" = @quantityInStockKg " +
                "WHERE \"Id\" = @id";

            NpgsqlCommand command = new NpgsqlCommand(commandText, connection);

            command.Parameters.AddWithValue("id", id);
            command.Parameters.AddWithValue("name", food.Name);
            command.Parameters.AddWithValue("pricePerKg", food.PricePerKg);
            command.Parameters.AddWithValue("quantityInStockKg", food.QuantityInStockKg);

            int affectedRows = await command.ExecuteNonQueryAsync();

            connection.Close();

            return affectedRows > 0;
        }

        public async Task<bool> DeleteFoodAsync(int id)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();

            string commandText =
                "DELETE FROM \"Food\" WHERE \"Id\" = @id";

            NpgsqlCommand command = new NpgsqlCommand(commandText, connection);
            command.Parameters.AddWithValue("id", id);

            int affectedRows = await command.ExecuteNonQueryAsync();

            connection.Close();

            return affectedRows > 0;
        }

    }
}

