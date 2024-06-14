using EmployeeManagement.Shared.Constants;
using EmployeeManagement.Shared.ViewModels.Employee;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Shared.ViewModels.Task
{
    public class TaskViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public Guid EmployeeId { get; set; }
        public EmployeeViewModel? Employee { get; set; }
    }
}
