using EmployeeManagement.Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Shared.ViewModels.Department
{
    public class DepartmentViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
