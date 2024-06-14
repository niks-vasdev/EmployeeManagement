using EmployeeManagement.Shared.Response;
using EmployeeManagement.Shared.ViewModels.Department;

namespace EmployeeManagement.Services.DepartmentService
{
    public interface IDepartmentService
    {
        Task<BaseResponse<List<DepartmentViewModel>>> GetAllDepartments();
        Task<BaseResponse<DepartmentViewModel>> GetDepartmentById(Guid id);
        Task<BaseResponse<Guid>> AddDepartment(AddDepartmentViewModel departmentViewModel);
        Task<BaseResponse<Guid>> UpdateDepartment(AddDepartmentViewModel departmentViewModel);
        Task<BaseResponse<bool>> DeleteDepartment(Guid departmentId);
    }
}
