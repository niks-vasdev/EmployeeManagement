using EmployeeManagement.Data.Models;
using EmployeeManagement.Shared.Response;
using EmployeeManagement.Shared.ViewModels.Employee;

namespace EmployeeManagement.Service.Services.EmployeeService
{
    public interface IEmployeeService
    {
        Task<BaseResponse<List<Employee>>> GetAllEmployee();
        Task<BaseResponse<Employee>> GetEmployeeById(Guid id);
        Task<BaseResponse<Guid>> AddEmployee(AddEmployeeViewModel employeeViewModel);
        Task<BaseResponse<Guid>> UpdateEmployee(AddEmployeeViewModel employeeViewModel);
        Task<BaseResponse<bool>> DeleteEmployee(Guid employeeId);
    }
}
