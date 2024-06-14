using Azure.Core;
using EmployeeManagement.Service.Services.DepartmentService;
using EmployeeManagement.Shared.ViewModels.Department;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace EmployeeManagement.API.Controllers
{
    /// <summary>
    /// Controller for managing department operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly IValidator<AddDepartmentViewModel> _validator;

        /// <summary>
        /// Constructor to initialize DepartmentController with IDepartmentService.
        /// </summary>
        /// <param name="departmentService">Service responsible for department operations.</param>
        public DepartmentController(IDepartmentService departmentService , IValidator<AddDepartmentViewModel> validator)
        {
            _departmentService = departmentService;
            _validator = validator;
        }

        /// <summary>
        /// Retrieves all departments.
        /// </summary>
        /// <returns>ActionResult with list of departments or error message.</returns>
        [HttpGet("GetAllDepartment")]
        public async Task<IActionResult> GetAllDepartment()
        {
            try
            {
                var result = await _departmentService.GetAllDepartments();
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
        /// Retrieves a department by its ID.
        /// </summary>
        /// <param name="id">ID of the department to retrieve.</param>
        /// <returns>ActionResult with department or error message.</returns>
        [HttpGet("GetDepartmentById/{Id:guid}")]
        public async Task<IActionResult> GetDepartmentById(Guid id)
        {
            try
            {
                var result = await _departmentService.GetDepartmentById(id);
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
        /// Adds a new department.
        /// </summary>
        /// <param name="viewModel">DepartmentViewModel containing department data.</param>
        /// <returns>ActionResult with success or error message.</returns>
        [HttpPost("AddDepartment")]
        public async Task<IActionResult> AddDepartment([FromBody] AddDepartmentViewModel viewModel)
        {
            try
            {
                var valid = _validator.Validate(viewModel, options => options.IncludeRuleSets("Add"));

                if (valid.IsValid)
                {
                    var result = await _departmentService.AddDepartment(viewModel);
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
        /// Updates an existing department.
        /// </summary>
        /// <param name="viewModel">DepartmentViewModel containing updated department data.</param>
        /// <returns>ActionResult with success or error message.</returns>
        [HttpPut("UpdateDepartment")]
        public async Task<IActionResult> UpdateDepartment([FromBody] AddDepartmentViewModel viewModel)
        {
            try
            {
                var valid = _validator.Validate(viewModel, options => options.IncludeRuleSets("Update"));

                if (valid.IsValid)
                {
                    var result = await _departmentService.UpdateDepartment(viewModel);
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
        /// Deletes a department by its ID.
        /// </summary>
        /// <param name="Id">ID of the department to delete.</param>
        /// <returns>ActionResult with success or error message.</returns>
        [HttpDelete("DeleteDepartment/{Id:guid}")]
        public async Task<IActionResult> DeleteDepartment(Guid id)
        {
            try
            {
                var result = await _departmentService.DeleteDepartment(id);
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
