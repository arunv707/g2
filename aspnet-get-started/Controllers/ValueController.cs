using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using G2App.Models;

namespace G2App.Controllers
{
    public class ValueController : Controller
    {
        private readonly IConfiguration _configuration;

        public ValueController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            string value = string.Empty;
            string connectionString = _configuration.GetConnectionString("SqlConnectionString");

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT TOP 1 columnx FROM tablex"; // Replace with your table/column
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        value = command.ExecuteScalar()?.ToString() ?? "No data found";
                    }
                }
            }
            catch (Exception ex)
            {
                value = $"Error: {ex.Message}";
            }

            var model = new ValueModel { DisplayValue = value };
            return View(model);
        }
    }
}
