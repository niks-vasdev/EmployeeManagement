using EmployeeManagement.Data.Common;
using EmployeeManagement.Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Data.Models
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}