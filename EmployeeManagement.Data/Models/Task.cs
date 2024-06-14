using EmployeeManagement.Data.Common;
using EmployeeManagement.Shared.Constants;
using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Data.Models
{
    public class Task : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public Guid EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}
