using Microsoft.AspNetCore.Mvc;
using HRManagementSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;

namespace HRManagementSystem.Controllers
{
    public class DepositDashboardController : Controller
    {
        private readonly string _connectionString;

        public DepositDashboardController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IActionResult Deposit()
        {
            var model = new DepositViewModel
            {
                Employees = GetEmployeeList()
            };
            return View(model);
        }

            private List<SelectListItem> GetEmployeeList()
        {
            var employees = new List<SelectListItem>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
                    SELECT e.Eid, ed.name 
                    FROM Employee e 
                    INNER JOIN EmployeeDetails ed ON e.Eid = ed.Eid 
                    ORDER BY ed.name";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employees.Add(new SelectListItem
                            {
                                Value = reader["Eid"].ToString(),
                                Text = reader["name"].ToString()
                            });
                        }
                    }
                }
            }

            return employees;
        }

        [HttpPost]
        public IActionResult AddDeposit(DepositViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Employees = GetEmployeeList();
                return View(model);
            }

                string insertQuery = "INSERT INTO Amount (Eid, amount, paymentDate) VALUES (@Eid, @Amount, @PaymentDate)";

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(insertQuery, conn);
                    cmd.Parameters.AddWithValue("@Eid", model.Eid);
                    cmd.Parameters.AddWithValue("@amount", model.Amount);
                    cmd.Parameters.AddWithValue("@paymentDate", model.PaymentDate);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            TempData["SuccessMessage"] = "Payroll added successfully!";

            return Redirect("/DepositDashboard/Deposit");

        }
    }
}