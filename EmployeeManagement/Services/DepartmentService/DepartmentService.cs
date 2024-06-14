using EmployeeManagement.ApiManager;
using EmployeeManagement.Shared.Response;
using EmployeeManagement.Shared.ViewModels.Department;

namespace EmployeeManagement.Services.DepartmentService
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IConfiguration _configuration;
        private readonly IApiManager _apiManager;

        public DepartmentService(IConfiguration configuration, IApiManager apiManager)
        {
            _configuration = configuration;
            _apiManager = apiManager;
        }

        public async Task<BaseResponse<Guid>> AddDepartment(AddDepartmentViewModel departmentViewModel)
        {
            try
            {
                var response = await _apiManager.PostAsync<BaseResponse<Guid>>(
                    $"{_configuration["BackendUrl"]}Department/AddDepartment", departmentViewModel);
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Guid> { IsSuccess = false, Message = ex.Message };
            }
        }

        public async Task<BaseResponse<bool>> DeleteDepartment(Guid departmentId)
        {
            try
            {
                var response = await _apiManager.DeleteAsync<BaseResponse<bool>>(
                    $"{_configuration["BackendUrl"]}Department/DeleteDepartment/{departmentId}");
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool> { IsSuccess = false, Message = ex.Message };
            }
        }

        public async Task<BaseResponse<List<DepartmentViewModel>>> GetAllDepartments()
        {
            try
            {
                var response = await _apiManager.GetAsync<BaseResponse<List<DepartmentViewModel>>>(
                    $"{_configuration["BackendUrl"]}Department/GetAllDepartment");
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<DepartmentViewModel>> { IsSuccess = false, Message = ex.Message };
            }
        }

        public async Task<BaseResponse<DepartmentViewModel>> GetDepartmentById(Guid id)
        {
            try
            {
                var response = await _apiManager.GetAsync<BaseResponse<DepartmentViewModel>>(
                    $"{_configuration["BackendUrl"]}Department/GetDepartmentById/{id}");
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse<DepartmentViewModel> { IsSuccess = false, Message = ex.Message };
            }
        }

        public async Task<BaseResponse<Guid>> UpdateDepartment(AddDepartmentViewModel departmentViewModel)
        {
            try
            {
                var response = await _apiManager.PutAsync<BaseResponse<Guid>>(
                    $"{_configuration["BackendUrl"]}Department/UpdateDepartment", departmentViewModel);
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Guid> { IsSuccess = false, Message = ex.Message };
            }
        }
    }
}
