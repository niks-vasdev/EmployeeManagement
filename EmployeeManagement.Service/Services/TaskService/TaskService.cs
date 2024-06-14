using AutoMapper;
using EmployeeManagement.Service.Repository;
using EmployeeManagement.Shared.Constants;
using EmployeeManagement.Shared.Response;
using EmployeeManagement.Shared.ViewModels.Task;
using Microsoft.EntityFrameworkCore;
using Tasks = EmployeeManagement.Data.Models.Task;

namespace EmployeeManagement.Service.Services.TaskService
{
    /// <summary>
    /// Service class for handling task-related operations.
    /// </summary>
    public class TaskService : ITaskService
    {
        private readonly IRepository<Tasks> _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor to initialize TaskService with repository and mapper.
        /// </summary>
        /// <param name="repository">Repository for database operations.</param>
        /// <param name="mapper">Automapper instance for mapping view models to entities.</param>
        public TaskService(IRepository<Tasks> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Adds a new task.
        /// </summary>
        /// <param name="taskViewModel">View model containing task data.</param>
        /// <returns>BaseResponse indicating success or failure of the operation.</returns>
        public async Task<BaseResponse<Guid>> AddTask(AddTaskViewModel taskViewModel)
        {
            try
            {
                var model = _mapper.Map<Tasks>(taskViewModel);
                model.Id = Guid.NewGuid();
                model.CreatedOn = DateTime.UtcNow;
                await _repository.AddAsync(model);
                return new BaseResponse<Guid>
                {
                    IsSuccess = true,
                    Message = ConstantString.TaskAddSuccess,
                    Data = model.Id
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Deletes a task by ID.
        /// </summary>
        /// <param name="taskId">ID of the task to delete.</param>
        /// <returns>BaseResponse indicating success or failure of the operation.</returns>
        public async Task<BaseResponse<bool>> DeleteTask(Guid taskId)
        {
            try
            {
                var model = await _repository.GetByIdAsync(taskId);
                if (model == null)
                {
                    return new BaseResponse<bool>
                    {
                        IsSuccess = false,
                        Message = string.Format(ConstantString.TaskNotFound, taskId),
                        Data = false
                    };
                }
                await _repository.DeleteAsync(model);
                return new BaseResponse<bool>
                {
                    IsSuccess = true,
                    Message= ConstantString.TaskDeleteSuccess,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Retrieves all tasks.
        /// </summary>
        /// <returns>BaseResponse containing a list of tasks or error information.</returns>
        public async Task<BaseResponse<List<Tasks>>> GetAllTask()
        {
            try
            {
                var list = _repository.ListAll().Include(x => x.Employee).ToList();
                if (list == null)
                {
                    return new BaseResponse<List<Tasks>>
                    {
                        IsSuccess = false,
                        Message = ConstantString.TaskGetAllError
                    };
                }
                return new BaseResponse<List<Tasks>>
                {
                    IsSuccess = true,
                    Data = list
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Retrieves a task by ID.
        /// </summary>
        /// <param name="id">ID of the task to retrieve.</param>
        /// <returns>BaseResponse containing the task or error information.</returns>
        public async Task<BaseResponse<Tasks>> GetTaskById(Guid id)
        {
            try
            {
                var model = await _repository.GetByIdAsync(id);
                if (model == null)
                {
                    return new BaseResponse<Tasks>
                    {
                        IsSuccess = false,
                        Message = string.Format(ConstantString.TaskNotFound, id),
                    };
                }
                return new BaseResponse<Tasks>
                {
                    IsSuccess = true,
                    Data = model
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Updates an existing task.
        /// </summary>
        /// <param name="taskViewModel">View model containing updated task data.</param>
        /// <returns>BaseResponse indicating success or failure of the operation.</returns>
        public async Task<BaseResponse<Guid>> UpdateTask(AddTaskViewModel taskViewModel)
        {
            try
            {
                var existingData = await _repository.GetByIdAsync(taskViewModel.Id);
                if (existingData == null)
                {
                    return new BaseResponse<Guid>
                    {
                        IsSuccess = false,
                        Message = ConstantString.TaskGetByIdError,
                    };
                }
                _mapper.Map(taskViewModel, existingData);
                existingData.LastModifiedOn = DateTime.UtcNow;
                await _repository.UpdateAsync(existingData);
                return new BaseResponse<Guid>
                {
                    IsSuccess = true,
                    Message = ConstantString.TaskUpdateSuccess,
                    Data = existingData.Id
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
