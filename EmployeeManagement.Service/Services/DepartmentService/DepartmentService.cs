using AutoMapper;
using EmployeeManagement.Data.Models;
using EmployeeManagement.Service.Repository;
using EmployeeManagement.Shared.Constants;
using EmployeeManagement.Shared.Response;
using EmployeeManagement.Shared.ViewModels.Department;

namespace EmployeeManagement.Service.Services.DepartmentService
{
    /// <summary>
    /// Service class for handling operations related to departments.
    /// </summary>
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepository<Department> _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor to initialize DepartmentService with repository and mapper.
        /// </summary>
        /// <param name="repository">Repository for database operations.</param>
        /// <param name="mapper">Automapper instance for mapping view models to entities.</param>
        public DepartmentService(IRepository<Department> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Adds a new department.
        /// </summary>
        /// <param name="departmentViewModel">View model containing department data.</param>
        /// <returns>BaseResponse indicating success or failure of the operation.</returns>
        public async Task<BaseResponse<Guid>> AddDepartment(AddDepartmentViewModel departmentViewModel)
        {
            try
            {
                var model = _mapper.Map<Department>(departmentViewModel);
                model.Id = Guid.NewGuid();
                model.CreatedOn = DateTime.UtcNow;
                await _repository.AddAsync(model);
                return new BaseResponse<Guid>
                {
                    IsSuccess = true,
                    Message = ConstantString.DepartmentAddSuccess,
                    Data = model.Id
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Deletes a department by its ID.
        /// </summary>
        /// <param name="departmentId">ID of the department to delete.</param>
        /// <returns>BaseResponse indicating success or failure of the operation.</returns>
        public async Task<BaseResponse<bool>> DeleteDepartment(Guid departmentId)
        {
            try
            {
                var model = await _repository.GetByIdAsync(departmentId);
                if (model == null)
                {
                    return new BaseResponse<bool>
                    {
                        IsSuccess = false,
                        Message = string.Format(ConstantString.DepartmentNotFound, departmentId),
                        Data = false
                    };
                }
                await _repository.DeleteAsync(model);
                return new BaseResponse<bool>
                {
                    IsSuccess = true,
                    Message = ConstantString.DepartmentDeleteSuccess,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Retrieves all departments.
        /// </summary>
        /// <returns>BaseResponse containing a list of departments or error information.</returns>
        public async Task<BaseResponse<List<Department>>> GetAllDepartments()
        {
            try
            {
                var list = (await _repository.ListAllAsync()).ToList();
                if (list == null)
                {
                    return new BaseResponse<List<Department>>
                    {
                        IsSuccess = false,
                        Message = ConstantString.DepartmentFetchFailed,
                    };
                }
                return new BaseResponse<List<Department>>
                {
                    IsSuccess = true,
                    Data = list,
                    Message = ConstantString.DepartmentFetchSuccess
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Retrieves a department by its ID.
        /// </summary>
        /// <param name="id">ID of the department to retrieve.</param>
        /// <returns>BaseResponse containing the department or error information.</returns>
        public async Task<BaseResponse<Department>> GetDepartmentById(Guid id)
        {
            try
            {
                var model = await _repository.GetByIdAsync(id);
                if (model == null)
                {
                    return new BaseResponse<Department>
                    {
                        IsSuccess = false,
                        Message = string.Format(ConstantString.DepartmentNotFound, id)
                    };
                }
                return new BaseResponse<Department>
                {
                    IsSuccess = true,
                    Message = ConstantString.DepartmentFetchSuccess,
                    Data = model
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Updates an existing department.
        /// </summary>
        /// <param name="departmentViewModel">View model containing updated department data.</param>
        /// <returns>BaseResponse indicating success or failure of the operation.</returns>
        public async Task<BaseResponse<Guid>> UpdateDepartment(AddDepartmentViewModel departmentViewModel)
        {
            try
            {
                var existingData = await _repository.GetByIdAsync(departmentViewModel.Id);
                if (existingData == null)
                {
                    return new BaseResponse<Guid>
                    {
                        IsSuccess = false,
                        Message = string.Format(ConstantString.DepartmentNotFound, departmentViewModel.Id) // Department not found message
                    };
                }
                _mapper.Map(departmentViewModel, existingData);
                existingData.LastModifiedOn = DateTime.UtcNow;
                await _repository.UpdateAsync(existingData);
                return new BaseResponse<Guid>
                {
                    IsSuccess = true,
                    Message = ConstantString.DepartmentUpdateSuccess,
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
