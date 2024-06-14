using EmployeeManagement.Service.Services.EmployeeService;
using EmployeeManagement.Shared.ViewModels.Department;
using EmployeeManagement.Shared.ViewModels.Employee;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    /// <summary>
    /// Controller for managing employee operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IValidator<AddEmployeeViewModel> _validator;

        public EmployeeController(IEmployeeService employeeService, IValidator<AddEmployeeViewModel> validator)
        {
            _employeeService = employeeService;
            _validator = validator;
        }

        /// <summary>
        /// Retrieves all employees.
        /// </summary>
        /// <returns>ActionResult with list of employees or error message.</returns>
        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                var result = await _employeeService.GetAllEmployee();
                if (!result.IsSuccess)
                {
                    return BadRequest(result); // Return 400 Bad Request with response object
                }
                return Ok(result); // Return 200 OK with response object
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Return 400 Bad Request with exception message
            }
        }

        /// <summary>
        /// Retrieves an employee by their ID.
        /// </summary>
        /// <param name="Id">ID of the employee to retrieve.</param>
        /// <returns>ActionResult with employee or error message.</returns>
        [HttpGet("GetEmployeeById/{Id:guid}")]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            try
            {
                var result = await _employeeService.GetEmployeeById(id);
                if (!result.IsSuccess)
                {
                    return BadRequest(result); // Return 400 Bad Request with response object
                }
                return Ok(result); // Return 200 OK with response object
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Return 400 Bad Request with exception message
            }
        }

        /// <summary>
        /// Adds a new employee.
        /// </summary>
        /// <param name="viewModel">EmployeeViewModel containing employee data.</param>
        /// <returns>ActionResult with success or error message.</returns>
        [HttpPost("AddEmployee")]
        public async Task<IActionResult> AddEmployee([FromBody] AddEmployeeViewModel viewModel)
        {
            try
            {
                var valid = _validator.Validate(viewModel, options => options.IncludeRuleSets("Add"));

                if (valid.IsValid)
                {
                    var result = await _employeeService.AddEmployee(viewModel);
                    if (!result.IsSuccess)
                    {
                        return BadRequest(result); // Return 400 Bad Request with response object
                    }
                    return Ok(result); // Return 200 OK with response object
                }
                
                return BadRequest(valid);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Return 400 Bad Request with exception message
            }
        }

        /// <summary>
        /// Updates an existing employee.
        /// </summary>
        /// <param name="viewModel">EmployeeViewModel containing updated employee data.</param>
        /// <returns>ActionResult with success or error message.</returns>
        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee([FromBody] AddEmployeeViewModel viewModel)
        {
            try
            {
                var valid = _validator.Validate(viewModel, options => options.IncludeRuleSets("Update"));

                if (valid.IsValid)
                {
                    var result = await _employeeService.UpdateEmployee(viewModel);
                    if (!result.IsSuccess)
                    {
                        return BadRequest(result); // Return 400 Bad Request with response object
                    }
                    return Ok(result); // Return 200 OK with response object
                }
                
                return BadRequest(valid);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Return 400 Bad Request with exception message
            }
        }

        /// <summary>
        /// Deletes an employee by their ID.
        /// </summary>
        /// <param name="Id">ID of the employee to delete.</param>
        /// <returns>ActionResult with success or error message.</returns>
        [HttpDelete("DeleteEmployee/{Id:guid}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            try
            {
                var result = await _employeeService.DeleteEmployee(id);
                if (!result.IsSuccess)
                {
                    return BadRequest(result); // Return 400 Bad Request with response object
                }
                return Ok(result); // Return 200 OK with response object
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Return 400 Bad Request with exception message
            }
        }
    }
}
