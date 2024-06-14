using EmployeeManagement.Data.Models;
using EmployeeManagement.Shared.Response;
using EmployeeManagement.Shared.ViewModels.Department;

namespace EmployeeManagement.Service.Services.DepartmentService
{
    public interface IDepartmentService
    {
        Task<BaseResponse<List<Department>>> GetAllDepartments();
        Task<BaseResponse<Department>> GetDepartmentById(Guid id);
        Task<BaseResponse<Guid>> AddDepartment(AddDepartmentViewModel departmentViewModel);
        Task<BaseResponse<Guid>> UpdateDepartment(AddDepartmentViewModel departmentViewModel);
        Task<BaseResponse<bool>> DeleteDepartment(Guid departmentId);

    }
}