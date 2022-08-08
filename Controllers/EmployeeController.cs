using InventoryManSys.Data;
using InventoryManSys.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManSys.Controllers
{
    [Authorize]
    [Route("Employee")]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _Db;

        public EmployeeController(ApplicationDbContext db)
        {
            _Db = db;
        }

        public IActionResult Index()
        {
            var users = _Db.Users.ToList();

            List<EmployeeVM> employees = new List<EmployeeVM>();

            foreach (var user in users)
            {
                EmployeeVM emp = new EmployeeVM();

                emp.Id = user.Id;
                emp.UserName = user.UserName;
                emp.PhoneNumber = user.PhoneNumber;
                emp.Email = user.Email;
                emp.EmailConfirmed = user.EmailConfirmed;

                employees.Add(emp);
            }

            return View(employees);
        }
    }
}
