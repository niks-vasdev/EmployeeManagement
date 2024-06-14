using EmployeeManagement.Shared.Constants;
using EmployeeManagement.Shared.ViewModels.Department;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Shared.ViewModels.Employee
{
    public class EmployeeViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime HireDate { get; set; }
        public Guid DepartmentId { get; set; }
        public DepartmentViewModel? Department { get; set; }
    }
}
