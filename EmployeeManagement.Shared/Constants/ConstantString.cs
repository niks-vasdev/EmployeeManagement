using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Shared.Constants
{
    public class ConstantString
    {
        // Department Required Validations Constants 
        public const string DepartmentName = "Department Name is required.";
        public const string DepartmentDescription = "Department Description is required.";

        // Employee Required Validations Constants 
        public const string EmployeeFirstName = "Employee First Name is required.";
        public const string EmployeeLastName = "Employee Last Name is required.";
        public const string EmployeeEmail = "Employee Email is required.";
        public const string EmployeePhone = "Employee Phone is required.";
        public const string EmployeeHireDate = "Employee Hire Date is required.";
        public const string EmployeeDepartmentId = "Employee Department Id is required.";

        // Task Required Validations Constants 
        public const string TaskName = "Task Name is required.";
        public const string TaskDescription = "Task Description is required.";
        public const string TaskTitle = "Task Title is required.";
        public const string TaskDueDate = "Task Due Date is required.";
        public const string TaskEmployeeId = "Task Employee Id is required.";

        // Task Service Error Messages
        public const string TaskAddError = "Failed to add task.";
        public const string TaskDeleteError = "Failed to delete task.";
        public const string TaskGetAllError = "Failed to retrieve all tasks.";
        public const string TaskGetByIdError = "Failed to retrieve task by ID.";
        public const string TaskUpdateError = "Failed to update task.";

        // Employee Service Error Messages
        public const string EmployeeAddError = "Failed to add employee.";
        public const string EmployeeDeleteError = "Failed to delete employee.";
        public const string EmployeeGetAllError = "Failed to retrieve all employee.";
        public const string EmployeeGetByIdError = "Failed to retrieve employee by ID.";
        public const string EmployeeUpdateError = "Failed to update employee.";

        // Department Service Error Messages
        public const string DepartmentAddError = "Failed to add department.";
        public const string DepartmentDeleteError = "Failed to delete department.";
        public const string DepartmentGetAllError = "Failed to retrieve all department.";
        public const string DepartmentGetByIdError = "Failed to retrieve department by ID.";
        public const string DepartmentUpdateError = "Failed to update department.";


        // Department Messages
        public const string DepartmentFetchFailed = "Failed to fetch departments.";
        public const string DepartmentFetchSuccess = "Departments fetched successfully.";
        public const string DepartmentNotFound = "Department with ID {0} not found.";
        public const string DepartmentAddFailed = "Failed to add department: {0}";
        public const string DepartmentAddSuccess = "Department added successfully.";
        public const string DepartmentUpdateFailed = "Failed to update department: {0}";
        public const string DepartmentUpdateSuccess = "Department updated successfully.";
        public const string DepartmentDeleteFailed = "Failed to delete department: {0}";
        public const string DepartmentDeleteSuccess = "Department deleted successfully.";

        // Employee Messages
        public const string EmployeeFetchFailed = "Failed to fetch employees.";
        public const string EmployeeFetchSuccess = "Employees fetched successfully.";
        public const string EmployeeNotFound = "Employee with ID {0} not found.";
        public const string EmployeeAddFailed = "Failed to add employee: {0}";
        public const string EmployeeAddSuccess = "Employee added successfully.";
        public const string EmployeeUpdateFailed = "Failed to update employee: {0}";
        public const string EmployeeUpdateSuccess = "Employee updated successfully.";
        public const string EmployeeDeleteFailed = "Failed to delete employee: {0}";
        public const string EmployeeDeleteSuccess = "Employee deleted successfully.";

        // Task Messages
        public const string TaskFetchFailed = "Failed to fetch tasks.";
        public const string TaskFetchSuccess = "Tasks fetched successfully.";
        public const string TaskNotFound = "Task with ID {0} not found.";
        public const string TaskAddFailed = "Failed to add task: {0}";
        public const string TaskAddSuccess = "Task added successfully.";
        public const string TaskUpdateFailed = "Failed to update task: {0}";
        public const string TaskUpdateSuccess = "Task updated successfully.";
        public const string TaskDeleteFailed = "Failed to delete task: {0}";
        public const string TaskDeleteSuccess = "Task deleted successfully.";

    }
}
