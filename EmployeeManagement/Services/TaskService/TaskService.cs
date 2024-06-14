using EmployeeManagement.ApiManager;
using EmployeeManagement.Shared.Response;
using EmployeeManagement.Shared.ViewModels.Task;

namespace EmployeeManagement.Services.TaskService
{
    public class TaskService : ITaskService
    {
        private readonly IConfiguration _configuration;
        private readonly IApiManager _apiManager;

        public TaskService(IConfiguration configuration, IApiManager apiManager)
        {
            _configuration = configuration;
            _apiManager = apiManager;
        }

        public async Task<BaseResponse<Guid>> AddTask(AddTaskViewModel taskViewModel)
        {
            try
            {
                var response = await _apiManager.PostAsync<BaseResponse<Guid>>(
                    $"{_configuration["BackendUrl"]}Task/AddTask", taskViewModel);
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Guid> { IsSuccess = false, Message = ex.Message };
            }
        }

        public async Task<BaseResponse<bool>> DeleteTask(Guid taskId)
        {
            try
            {
                var response = await _apiManager.DeleteAsync<BaseResponse<bool>>(
                    $"{_configuration["BackendUrl"]}Task/DeleteTask/{taskId}");
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool> { IsSuccess = false, Message = ex.Message };
            }
        }

        public async Task<BaseResponse<List<TaskViewModel>>> GetAllTask()
        {
            try
            {
                var response = await _apiManager.GetAsync<BaseResponse<List<TaskViewModel>>>(
                    $"{_configuration["BackendUrl"]}Task/GetAllTasks");
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<TaskViewModel>> { IsSuccess = false, Message = ex.Message };
            }
        }

        public async Task<BaseResponse<TaskViewModel>> GetTaskById(Guid id)
        {
            try
            {
                var response = await _apiManager.GetAsync<BaseResponse<TaskViewModel>>(
                    $"{_configuration["BackendUrl"]}Task/GetTaskById/{id}");
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse<TaskViewModel> { IsSuccess = false, Message = ex.Message };
            }
        }

        public async Task<BaseResponse<Guid>> UpdateTask(AddTaskViewModel taskViewModel)
        {
            try
            {
                var response = await _apiManager.PutAsync<BaseResponse<Guid>>(
                    $"{_configuration["BackendUrl"]}Task/UpdateTask", taskViewModel);
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Guid> { IsSuccess = false, Message = ex.Message };
            }
        }
    }
}
