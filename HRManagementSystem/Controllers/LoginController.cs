using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace HRManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Authenticate(string username, string password)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            string role = null;
            string displayName = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT userRole, Uid FROM [NovaGamesDB].[dbo].[Users] WHERE Uid = @Username AND userPassword = @Password";

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            role = reader["userRole"].ToString();
                            displayName = reader["Uid"].ToString();
                        }
                    }
                }
            }

            if (role == null)
            {
                return Unauthorized("Invalid credentials");
            }

            HttpContext.Session.SetString("LoggedInUser", displayName);

            if (role == "A")
            {
                return RedirectToAction("Admin", "AdminDashBoard");
            }
            else if (role == "E")
            {
                return RedirectToAction("Employee", "EmployeeDashboard");
            }
            else
            {
                return Unauthorized("Invalid role");
            }
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
