using EmployeeManagement.Services.DepartmentService;
using EmployeeManagement.Services.EmployeeService;
using EmployeeManagement.Shared.ViewModels.Employee;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;

        public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            var departments = await _departmentService.GetAllDepartments();
            return View(departments.Data);
        }

        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                var result = await _employeeService.GetAllEmployee();
                return new JsonResult(result.Data);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            try
            {
                var result = await _employeeService.GetEmployeeById(id);
                return new JsonResult(result.Data);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        public async Task<IActionResult> AddEmployee([FromBody] AddEmployeeViewModel viewModel)
        {
            try
            {
                var result = await _employeeService.AddEmployee(viewModel);
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        public async Task<IActionResult> UpdateEmployee([FromBody] AddEmployeeViewModel viewModel)
        {
            try
            {
                var result = await _employeeService.UpdateEmployee(viewModel);
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            try
            {
                var result = await _employeeService.DeleteEmployee(id);
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }
    }
}
