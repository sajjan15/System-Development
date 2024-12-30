using Microsoft.AspNetCore.Mvc;

namespace HRManagementSystem.Controllers
{
    public class AdminDashboardController : Controller
    {

        public IActionResult Admin()
        {
            ViewBag.UserName = HttpContext.Session.GetString("LoggedInUser");
            return View();
        }
        
        public IActionResult AddEmployee()
        {
            // Return the AddEmployee view
            return RedirectToAction("Index", "AddEmployeeDashboard");
        }

        public IActionResult AddDeposit()
        {
            // Return the AddDeposit view
            return RedirectToAction("Deposit", "DepositDashboard");
        }
    }
}
