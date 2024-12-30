using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using NovaGamesDB.Models;

namespace HRManagementSystem.Controllers
{
    public class AddEmployeeDashboardController : Controller
    {
        private readonly string _connectionString;

        public IActionResult Index()
        {
            // Your logic for the Index page
            return View();
        }

        public AddEmployeeDashboardController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //add employee model
        public IActionResult Add(AddEmployeeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); 
            }

            string insertUserQuery = @"
            INSERT INTO Users (userPassword, userRole) 
            OUTPUT INSERTED.Uid 
            VALUES (@userPassword, @userRole)";
            int newUid;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(insertUserQuery, connection);
                command.Parameters.AddWithValue("@userPassword", model.UserPassword); 
                command.Parameters.AddWithValue("@userRole", model.UserRole); 

                connection.Open();
                newUid = (int)command.ExecuteScalar();
                connection.Close();
            }
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                string insertEmployeeQuery = "INSERT INTO Employee (Uid) VALUES (@Uid)";
                SqlCommand command = new SqlCommand(insertEmployeeQuery, connection);
                command.Parameters.AddWithValue("@Uid", newUid);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // Query to get the Eid after inserting into Employee
                string getEidQuery = "SELECT TOP 1 Eid FROM Employee WHERE Uid = @Uid";

                SqlCommand getEidCommand = new SqlCommand(getEidQuery, connection);
                getEidCommand.Parameters.AddWithValue("@Uid", newUid);

                connection.Open();
                int newEid = (int)getEidCommand.ExecuteScalar();

                // Query to insert into EmployeeDetails
                string insertDetailsQuery = @"
                INSERT INTO EmployeeDetails (Eid, department, name, email)
                VALUES (@Eid, @department, @name, @email)";

                SqlCommand insertDetailsCommand = new SqlCommand(insertDetailsQuery, connection);
                insertDetailsCommand.Parameters.AddWithValue("@Eid", newEid);
                insertDetailsCommand.Parameters.AddWithValue("@department", model.Department);
                insertDetailsCommand.Parameters.AddWithValue("@name", model.Name);
                insertDetailsCommand.Parameters.AddWithValue("@email", model.Email);

                insertDetailsCommand.ExecuteNonQuery();
                connection.Close();
            }
            TempData["SuccessMessage"] = "Employee details added successfully!";

            return RedirectToAction("Index", "AddEmployeeDashboard");
        }

        
    }
}
