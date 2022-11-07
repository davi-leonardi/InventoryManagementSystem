using System.ComponentModel;

namespace InventoryManSys.Models.ViewModels
{
    public class EmployeeVM
    {
        public string Id { get; set; }
        [DisplayName("Username")]
        public string UserName { get; set; }
        public string Email { get; set; }
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }
        [DisplayName("Email Confirmed?")]
        public bool EmailConfirmed { get; set; }
    }
}
