using Microsoft.AspNetCore.Mvc.Rendering;

namespace HRManagementSystem.Models
{
    public class DepositViewModel
    {
        public int Eid { get; set; }

        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        public List<SelectListItem> Employees { get; set; } = new List<SelectListItem>(); 
    }
}