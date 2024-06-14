using EmployeeManagement.ApiManager;
using EmployeeManagement.Shared.Response;
using EmployeeManagement.Shared.ViewModels.Employee;

namespace EmployeeManagement.Services.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IConfiguration _configuration;
        private readonly IApiManager _apiManager;

        public EmployeeService(IConfiguration configuration, IApiManager apiManager)
        {
            _configuration = configuration;
            _apiManager = apiManager;
        }

        public async Task<BaseResponse<Guid>> AddEmployee(AddEmployeeViewModel employeeViewModel)
        {
            try
            {
                var response = await _apiManager.PostAsync<BaseResponse<Guid>>(
                    $"{_configuration["BackendUrl"]}Employee/AddEmployee", employeeViewModel);
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Guid> { IsSuccess = false, Message = ex.Message };
            }
        }

        public async Task<BaseResponse<bool>> DeleteEmployee(Guid employeeId)
        {
            try
            {
                var response = await _apiManager.DeleteAsync<BaseResponse<bool>>(
                    $"{_configuration["BackendUrl"]}Employee/DeleteEmployee/{employeeId}");
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool> { IsSuccess = false, Message = ex.Message };
            }
        }

        public async Task<BaseResponse<List<EmployeeViewModel>>> GetAllEmployee()
        {
            try
            {
                var response = await _apiManager.GetAsync<BaseResponse<List<EmployeeViewModel>>>(
                    $"{_configuration["BackendUrl"]}Employee/GetAllEmployees");
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<EmployeeViewModel>> { IsSuccess = false, Message = ex.Message };
            }
        }

        public async Task<BaseResponse<EmployeeViewModel>> GetEmployeeById(Guid id)
        {
            try
            {
                var response = await _apiManager.GetAsync<BaseResponse<EmployeeViewModel>>(
                    $"{_configuration["BackendUrl"]}Employee/GetEmployeeById/{id}");
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse<EmployeeViewModel> { IsSuccess = false, Message = ex.Message };
            }
        }

        public async Task<BaseResponse<Guid>> UpdateEmployee(AddEmployeeViewModel employeeViewModel)
        {
            try
            {
                var response = await _apiManager.PutAsync<BaseResponse<Guid>>(
                    $"{_configuration["BackendUrl"]}Employee/UpdateEmployee", employeeViewModel);
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Guid> { IsSuccess = false, Message = ex.Message };
            }
        }
    }
}
