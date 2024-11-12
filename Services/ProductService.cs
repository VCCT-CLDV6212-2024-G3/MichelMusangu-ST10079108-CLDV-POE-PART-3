using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using CloudP3.Models;

namespace CloudP3.Services
{
    public class ProductService
    {
        private readonly IConfiguration _configuration;

        public ProductService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task InsertProductAsync(ProductModel product)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            var query = @"INSERT INTO ProductTable (Name, Price, Category)
                          VALUES (@Name, @Price, @Category)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Category", product.Category);

                connection.Open();
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}

