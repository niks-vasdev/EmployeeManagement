using EmployeeManagement.Services.EmployeeService;
using EmployeeManagement.Services.TaskService;
using EmployeeManagement.Shared.ViewModels.Task;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly IEmployeeService _employeeService;

        public TaskController(ITaskService taskService, IEmployeeService employeeService)
        {
            _taskService = taskService;
            _employeeService = employeeService;
        }

        public async Task<IActionResult> Index()
        {
            var employeeList = await _employeeService.GetAllEmployee();
            return View(employeeList.Data);
        }

        public async Task<IActionResult> GetAllTasks()
        {
            try
            {
                var result = await _taskService.GetAllTask();
                return new JsonResult(result.Data);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        public async Task<IActionResult> GetTaskById(Guid id)
        {
            try
            {
                var result = await _taskService.GetTaskById(id);
                return new JsonResult(result.Data);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        public async Task<IActionResult> AddTask([FromBody] AddTaskViewModel viewModel)
        {
            try
            {
                var result = await _taskService.AddTask(viewModel);
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        public async Task<IActionResult> UpdateTask([FromBody] AddTaskViewModel viewModel)
        {
            try
            {
                var result = await _taskService.UpdateTask(viewModel);
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        public async Task<IActionResult> DeleteTask(Guid id)
        {
            try
            {
                var result = await _taskService.DeleteTask(id);
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }
    }
}
