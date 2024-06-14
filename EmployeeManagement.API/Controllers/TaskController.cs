using EmployeeManagement.Service.Services.TaskService;
using EmployeeManagement.Shared.ViewModels.Department;
using EmployeeManagement.Shared.ViewModels.Task;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    /// <summary>
    /// Controller for managing task operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IValidator<AddTaskViewModel> _validator;

        /// <summary>
        /// Constructor to initialize TaskController with ITaskService.
        /// </summary>
        /// <param name="taskService">Service responsible for task operations.</param>
        public TaskController(ITaskService taskService, IValidator<AddTaskViewModel> validator)
        {
            _taskService = taskService;
            _validator = validator;
        }

        /// <summary>
        /// Retrieves all tasks.
        /// </summary>
        /// <returns>ActionResult with list of tasks or error message.</returns>
        [HttpGet("GetAllTasks")]
        public async Task<IActionResult> GetAllTasks()
        {
            try
            {
                var result = await _taskService.GetAllTask();
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
        /// Retrieves a task by its ID.
        /// </summary>
        /// <param name="id">ID of the task to retrieve.</param>
        /// <returns>ActionResult with task or error message.</returns>
        [HttpGet("GetTaskById/{Id:guid}")]
        public async Task<IActionResult> GetTaskById(Guid id)
        {
            try
            {
                var result = await _taskService.GetTaskById(id);
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
        /// Adds a new task.
        /// </summary>
        /// <param name="viewModel">TaskViewModel containing task data.</param>
        /// <returns>ActionResult with success or error message.</returns>
        [HttpPost("AddTask")]
        public async Task<IActionResult> AddTask([FromBody]AddTaskViewModel viewModel)
        {
            try
            {
                var valid = _validator.Validate(viewModel, options => options.IncludeRuleSets("Add"));

                if (valid.IsValid)
                {
                    var result = await _taskService.AddTask(viewModel);
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
        /// Updates an existing task.
        /// </summary>
        /// <param name="viewModel">TaskViewModel containing updated task data.</param>
        /// <returns>ActionResult with success or error message.</returns>
        [HttpPut("UpdateTask")]
        public async Task<IActionResult> UpdateTask(AddTaskViewModel viewModel)
        {
            try
            {
                var valid = _validator.Validate(viewModel, options => options.IncludeRuleSets("Update"));

                if (valid.IsValid)
                {
                    var result = await _taskService.UpdateTask(viewModel);
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
        /// Deletes a task by its ID.
        /// </summary>
        /// <param name="id">ID of the task to delete.</param>
        /// <returns>ActionResult with success or error message.</returns>
        [HttpDelete("DeleteTask/{Id:guid}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            try
            {
                var result = await _taskService.DeleteTask(id);
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
