using EmployeeData.Models;
using EmployeeData.Repo;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeData.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            IEnumerable<EmployeeModel> employees = new List<EmployeeModel>();
            try
            {
                EmployeeRepo employeeRepo = new EmployeeRepo();
                employees = employeeRepo.GetAllEmployeeList();
                
                foreach (var employee in employees)
                {
                    employee.DearnessAllowance = employee.BasicSalary * 0.4f; ;
                    employee.ConveyanceAllowance = CalculateConveyanceAllowance(employee);
                    employee.HouseRentAllowance = CalculateHouseRentAllowance(employee);
                    employee.PT = CalculatePT(employee);
                    employee.TotalSalary = CalculateTotalSalary(employee);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }

            return View(employees);
        }

        private float CalculateTotalSalary(EmployeeModel employee)
        {
            float grossSalary = employee.BasicSalary + employee.DearnessAllowance + employee.ConveyanceAllowance + employee.HouseRentAllowance;

            float totalSalary = grossSalary - employee.PT;

            return totalSalary;
        }

        private float CalculatePT(EmployeeModel employee)
        {
            float grossSalary = employee.BasicSalary + employee.DearnessAllowance + employee.ConveyanceAllowance + employee.HouseRentAllowance;
           
            float pt;
            if (grossSalary <= 3000)
            {
                pt = 100;
            }
            else if (grossSalary > 3000 && grossSalary <= 6000)
            {
                pt = 150;
            }
            else
            {
                pt = 200;
            }

            return pt;
        }

        private float CalculateHouseRentAllowance(EmployeeModel employee)
        {
            float houseRentAllowance = Math.Max(employee.BasicSalary * 0.25f, 1500f);
            return houseRentAllowance;
        }

        private float CalculateConveyanceAllowance(EmployeeModel employee)
        {
            float conveyanceAllowance = Math.Min(employee.DearnessAllowance * 0.1f, 250f);

            return conveyanceAllowance;
        }
        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeModel employee)
        {
            try
            {
                EmployeeRepo employeeRepo = new EmployeeRepo();
                employeeRepo.SaveEmployeeDetails(employee, "Add");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(string id)
        {
            EmployeeModel employee = new EmployeeModel();
            try
            {
                EmployeeRepo employeeRepo = new EmployeeRepo();
                employee = employeeRepo.GetAllEmployeeList().Where(E => E.EmployeeID == Convert.ToInt32(id)).First();
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }

            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeModel employee)
        {
            try
            {
                EmployeeRepo employeeRepo = new EmployeeRepo();
                employeeRepo.SaveEmployeeDetails(employee,"EDIT");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(employee);
            }

            return RedirectToAction("index");

        }

    }//end of class HomeController

}//end of namespace EmployeeData.Controllers