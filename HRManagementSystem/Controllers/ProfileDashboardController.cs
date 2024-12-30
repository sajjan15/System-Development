using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using HRManagementSystem.Models;

namespace HRManagementSystem.Controllers
{
    public class ProfileDashboardController : Controller
    {
        private readonly string _connectionString;

        public ProfileDashboardController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IActionResult Profile()
        {
            string username = HttpContext.Session.GetString("LoggedInUser");

            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Login");
            }

            ProfileViewModel profileModel = GetUserProfile(username);

            if (profileModel == null)
            {
                return NotFound("User profile not found");
            }

            return View("ProfileIndex", profileModel);
        }

        private ProfileViewModel GetUserProfile(string username)
        {
            ProfileViewModel profileModel = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string profileQuery = @"
            SELECT u.[Uid], u.[userRole], e.[Eid], ed.[name], ed.[department], ed.[email]
            FROM [NovaGamesDB].[dbo].[Users] u
            LEFT JOIN [NovaGamesDB].[dbo].[Employee] e ON u.Uid = e.Uid
            LEFT JOIN [NovaGamesDB].[dbo].[EmployeeDetails] ed ON e.Eid = ed.Eid
            WHERE u.Uid = @Uid";

                using (SqlCommand command = new SqlCommand(profileQuery, connection))
                {
                    command.Parameters.AddWithValue("@Uid", username);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            profileModel = new ProfileViewModel
                            {
                                UserId = reader["Uid"].ToString(),
                                Role = reader["userRole"].ToString(),
                                EmployeeId = reader["Eid"].ToString(),
                                Name = reader["name"].ToString(),
                                Department = reader["department"].ToString(),
                                Email = reader["email"].ToString()
                            };
                        }
                    }
                }
            }

            return profileModel;
        }
    }
}