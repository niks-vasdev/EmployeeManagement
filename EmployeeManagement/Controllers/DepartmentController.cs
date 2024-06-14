using EmployeeManagement.Services.DepartmentService;
using EmployeeManagement.Shared.ViewModels.Department;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAllDepartments()
        {
            try
            {
                var result = await _departmentService.GetAllDepartments();
                return new JsonResult(result.Data);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        public async Task<IActionResult> GetDepartmentById(Guid id)
        {
            try
            {
                var result = await _departmentService.GetDepartmentById(id);
                return new JsonResult(result.Data);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        public async Task<IActionResult> AddDepartment([FromBody] AddDepartmentViewModel viewModel)
        {
            try
            {
                var result = await _departmentService.AddDepartment(viewModel);
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        public async Task<IActionResult> UpdateDepartment([FromBody] AddDepartmentViewModel viewModel)
        {
            try
            {
                var result = await _departmentService.UpdateDepartment(viewModel);
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        public async Task<IActionResult> DeleteDepartment(Guid id)
        {
            try
            {
                var result = await _departmentService.DeleteDepartment(id);
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }
    }
}
