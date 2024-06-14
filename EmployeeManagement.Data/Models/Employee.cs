using EmployeeManagement.Data.Common;
using EmployeeManagement.Shared.Constants;
using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Data.Models
{
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime HireDate { get; set; }
        public Guid DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}
