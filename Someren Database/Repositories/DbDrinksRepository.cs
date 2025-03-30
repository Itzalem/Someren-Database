using Microsoft.Data.SqlClient;
using Someren_Database.Models;

namespace Someren_Database.Repositories
{
	public class DbDrinksRepository : IDrinksRepository
	{
		private readonly string? _connectionString;

		public DbDrinksRepository(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("Someren_Database");
			if (string.IsNullOrEmpty(_connectionString))
			{
				_connectionString = "Server=your_server;Database=your_database;User Id=your_user;Password=your_password;";

			}
		}
		private Drink ReadDrink(SqlDataReader reader)
		{
			int drinkId = (int)reader["drink_id"];
			string name = (string)reader["name"];
			decimal vat = (decimal)reader["vat"];
			int stockOfDrink = (int)reader["stockOfDrink"];
			string type = (string)reader["type"];

			return new Drink(drinkId, name, vat, stockOfDrink, type);
		}

		public List<Drink> ListDrinks()
		{
			List<Drink> drinks = new List<Drink>();
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				string query = "SELECT drink_id, name, vat, stockOfDrink, type " +
								"FROM Drinks";

				SqlCommand command = new SqlCommand(query, connection);

				command.Connection.Open();
				SqlDataReader reader = command.ExecuteReader();

				while (reader.Read())
				{
					Drink drink = ReadDrink(reader);
					drinks.Add(drink);
				}
				reader.Close();
			}

			return drinks;

		}

		public void AddOrder(Order order)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				string query = $"INSERT INTO Orders (studentNumber, drink_id, amount) " +
								$"VALUES (@studentNumber, @drink_id, @amount);" +
								$"SELECT SCOPE_IDENTITY();";

				SqlCommand command = new SqlCommand(query, connection);

				command.Parameters.AddWithValue("@studentNumber", order.StudentNumber);
				command.Parameters.AddWithValue("@drink_id", order.DrinkId);
				command.Parameters.AddWithValue("@amount", order.Amount);

				command.Connection.Open();

				//to get the identity value of the scope 
				object result = command.ExecuteScalar(); 
				if (result != null)
				{
					order.OrderId = Convert.ToInt32(result);
				}
				else
				{
					throw new Exception("Ordering failed");
				}

			}

		}

		public void ReduceStock (Order order, Drink drink)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				string query = "UPDATE Drinks " +
							"SET stockOfDrink = stockOfDrink - @amount " +
                            "WHERE drink_id = @drink_id; ";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@amount", order.Amount);
                command.Parameters.AddWithValue("@drink_id", drink.DrinkId);

                command.Connection.Open();

                int rowsChanged = command.ExecuteNonQuery();
                if (rowsChanged != 1)
                {
                    throw new Exception("Stock not updated");
                }
            }

        }
	}
}
