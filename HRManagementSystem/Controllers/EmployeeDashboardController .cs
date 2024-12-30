using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace HRManagementSystem.Controllers
{
    public class EmployeeDashboardController : Controller
    {
        private readonly string _connectionString;


        public EmployeeDashboardController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IActionResult Employee()
        {
            string uid = HttpContext.Session.GetString("LoggedInUser");

            if (string.IsNullOrEmpty(uid))
            {
                return RedirectToAction("Login", "Login");
            }

            var employeeData = new List<dynamic>();
            int eid = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string eidQuery = "SELECT Eid FROM [NovaGamesDB].[dbo].[Employee] WHERE Uid = @Uid";
                using (SqlCommand command = new SqlCommand(eidQuery, connection))
                {
                    command.Parameters.AddWithValue("@Uid", uid);
                    var result = command.ExecuteScalar();
                    eid = Convert.ToInt32(result);
                }

                string detailsQuery = "SELECT [Eid], [name], [department], [email] FROM [NovaGamesDB].[dbo].[EmployeeDetails] WHERE Eid = @Eid";
                using (SqlCommand command = new SqlCommand(detailsQuery, connection))
                {
                    command.Parameters.AddWithValue("@Eid", eid);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employeeData.Add(new
                            {
                                Eid = reader["Eid"].ToString(),
                                Name = reader["name"].ToString(),
                                Department = reader["department"].ToString(),
                                Email = reader["email"].ToString()
                            });
                        }
                    }
                }
            }

            ViewData["EmployeeData"] = employeeData;
            return View();
        }
    }
}