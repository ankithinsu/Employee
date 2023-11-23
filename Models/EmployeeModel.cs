using System.ComponentModel.DataAnnotations;

namespace EmployeeData.Models
{
    public class EmployeeModel
    {
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Employee Code is required.")]
        public int EmployeeCode { get; set; }

        [Required(ErrorMessage = "Employee Name is required.")]
        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public bool Gender { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        public string Department { get; set; }

        [Required(ErrorMessage = "Designation is required.")]
        public string Designation { get; set; }

        [Required(ErrorMessage = "Basic Salary is required.")]
        [Range(0, float.MaxValue, ErrorMessage = "Basic Salary must be a positive number.")]
        public float BasicSalary { get; set; }

        public float DearnessAllowance { get; set; }

        public float ConveyanceAllowance { get; set; }

        public float HouseRentAllowance { get; set; }

        public float PT { get; set;}

        public float TotalSalary { get; set; }
    }
}
