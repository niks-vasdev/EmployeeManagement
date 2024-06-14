using AutoMapper;
using EmployeeManagement.Data.Models;
using EmployeeManagement.Service.Repository;
using EmployeeManagement.Shared.Constants;
using EmployeeManagement.Shared.Response;
using EmployeeManagement.Shared.ViewModels.Employee;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Service.Services.EmployeeService
{
    /// <summary>
    /// Service class for handling employee-related operations.
    /// </summary>
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor to initialize EmployeeService with repository and mapper.
        /// </summary>
        /// <param name="repository">Repository for database operations.</param>
        /// <param name="mapper">Automapper instance for mapping view models to entities.</param>
        public EmployeeService(IRepository<Employee> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Adds a new employee.
        /// </summary>
        /// <param name="employeeViewModel">View model containing employee data.</param>
        /// <returns>BaseResponse indicating success or failure of the operation.</returns>
        public async Task<BaseResponse<Guid>> AddEmployee(AddEmployeeViewModel employeeViewModel)
        {
            try
            {
                var model = _mapper.Map<Employee>(employeeViewModel);
                model.Id = Guid.NewGuid();
                model.CreatedOn = DateTime.UtcNow;
                await _repository.AddAsync(model);
                return new BaseResponse<Guid>
                {
                    IsSuccess = true,
                    Message = ConstantString.EmployeeAddSuccess,
                    Data = model.Id
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Deletes an employee by ID.
        /// </summary>
        /// <param name="employeeId">ID of the employee to delete.</param>
        /// <returns>BaseResponse indicating success or failure of the operation.</returns>
        public async Task<BaseResponse<bool>> DeleteEmployee(Guid employeeId)
        {
            try
            {
                var model = await _repository.GetByIdAsync(employeeId);
                if (model == null)
                {
                    return new BaseResponse<bool>
                    {
                        IsSuccess = false,
                        Message = string.Format(ConstantString.EmployeeNotFound, employeeId),
                        Data = false
                    };
                }
                await _repository.DeleteAsync(model);
                return new BaseResponse<bool>
                {
                    IsSuccess = true,
                    Message= ConstantString.EmployeeDeleteSuccess,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Retrieves all employees.
        /// </summary>
        /// <returns>BaseResponse containing a list of employees or error information.</returns>
        public async Task<BaseResponse<List<Employee>>> GetAllEmployee()
        {
            try
            {
                var list = _repository.ListAll().Include(x => x.Department).ToList();
                if (list == null)
                {
                    return new BaseResponse<List<Employee>>
                    {
                        IsSuccess = false,
                        Message = ConstantString.EmployeeGetAllError,
                    };
                }
                return new BaseResponse<List<Employee>>
                {
                    IsSuccess = true,
                    Data = list,
                    Message = ConstantString.EmployeeFetchSuccess
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Retrieves an employee by ID.
        /// </summary>
        /// <param name="id">ID of the employee to retrieve.</param>
        /// <returns>BaseResponse containing the employee or error information.</returns>
        public async Task<BaseResponse<Employee>> GetEmployeeById(Guid id)
        {
            try
            {
                var model = await _repository.GetByIdAsync(id);
                if (model == null)
                {
                    return new BaseResponse<Employee>
                    {
                        IsSuccess = false,
                        Message = string.Format(ConstantString.EmployeeNotFound, id)
                    };
                }
                return new BaseResponse<Employee>
                {
                    IsSuccess = true,
                    Data = model,
                    Message = ConstantString.EmployeeFetchSuccess
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Updates an existing employee.
        /// </summary>
        /// <param name="employeeViewModel">View model containing updated employee data.</param>
        /// <returns>BaseResponse indicating success or failure of the operation.</returns>
        public async Task<BaseResponse<Guid>> UpdateEmployee(AddEmployeeViewModel employeeViewModel)
        {
            try
            {
                var existingData = await _repository.GetByIdAsync(employeeViewModel.Id);
                if (existingData == null)
                {
                    return new BaseResponse<Guid>
                    {
                        IsSuccess = false,
                        Message = string.Format(ConstantString.EmployeeNotFound, employeeViewModel.Id)
                    };
                }
                _mapper.Map(employeeViewModel, existingData);
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
