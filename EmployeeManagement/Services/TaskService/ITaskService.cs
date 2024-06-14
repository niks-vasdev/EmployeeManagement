using EmployeeManagement.Shared.Response;
using EmployeeManagement.Shared.ViewModels.Task;

namespace EmployeeManagement.Services.TaskService
{
    public interface ITaskService
    {
        Task<BaseResponse<List<TaskViewModel>>> GetAllTask();
        Task<BaseResponse<TaskViewModel>> GetTaskById(Guid id);
        Task<BaseResponse<Guid>> AddTask(AddTaskViewModel taskViewModel);
        Task<BaseResponse<Guid>> UpdateTask(AddTaskViewModel taskViewModel);
        Task<BaseResponse<bool>> DeleteTask(Guid taskId);
    }
}
