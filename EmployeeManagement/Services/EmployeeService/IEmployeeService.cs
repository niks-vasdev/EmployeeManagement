using EmployeeManagement.Shared.Response;
using EmployeeManagement.Shared.ViewModels.Employee;

namespace EmployeeManagement.Services.EmployeeService
{
    public interface IEmployeeService
    {
        Task<BaseResponse<List<EmployeeViewModel>>> GetAllEmployee();
        Task<BaseResponse<EmployeeViewModel>> GetEmployeeById(Guid id);
        Task<BaseResponse<Guid>> AddEmployee(AddEmployeeViewModel employeeViewModel);
        Task<BaseResponse<Guid>> UpdateEmployee(AddEmployeeViewModel employeeViewModel);
        Task<BaseResponse<bool>> DeleteEmployee(Guid employeeId);
    }
}
