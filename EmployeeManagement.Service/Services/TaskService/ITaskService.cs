using EmployeeManagement.Shared.Response;
using EmployeeManagement.Shared.ViewModels.Task;


namespace EmployeeManagement.Service.Services.TaskService
{
    public interface ITaskService
    {
        Task<BaseResponse<List<Data.Models.Task>>> GetAllTask();
        Task<BaseResponse<Data.Models.Task>> GetTaskById(Guid id);
        Task<BaseResponse<Guid>> AddTask(AddTaskViewModel taskViewModel);
        Task<BaseResponse<Guid>> UpdateTask(AddTaskViewModel taskViewModel);
        Task<BaseResponse<bool>> DeleteTask(Guid taskId);
    }
}
