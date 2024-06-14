using EmployeeManagement.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmployeeManagement.Data.DataContext
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var departmentId = new Guid("99c274e2-ebe9-4305-b02b-bed1c6db2ba1");
            var employeeId = new Guid("1d117e28-e679-4807-b3dd-127211b09354");
            var taskId = new Guid("675e42c7-b7ce-4ac3-b91e-83b21e7ad5ec");

            modelBuilder.Entity<Department>().HasData(new Department
            {
                Id = departmentId,
                Name = "Department",
                Description = "Seeding Department",
            });

            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                Id = employeeId,
                FirstName = "Admin",
                LastName = "Seeding",
                DepartmentId = departmentId,
                Email = "admin@gmail.com",
                HireDate = DateTime.UtcNow,
                Phone = "987654321"
            });

            modelBuilder.Entity<Models.Task>().HasData(new Models.Task
            {
                Id = taskId,
                Name = "Task",
                Description = "Seeding Task",
                EmployeeId = employeeId,
                Title = "Title",
                DueDate = DateTime.UtcNow,
            });


        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
    }
}
