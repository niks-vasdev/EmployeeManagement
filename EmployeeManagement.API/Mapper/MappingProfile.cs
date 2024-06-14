using AutoMapper;
using EmployeeManagement.Data.Models;
using EmployeeManagement.Shared.ViewModels.Department;
using EmployeeManagement.Shared.ViewModels.Employee;
using EmployeeManagement.Shared.ViewModels.Task;
using Task = EmployeeManagement.Data.Models.Task;

namespace EmployeeManagement.API.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeViewModel, Employee>()
                .ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<TaskViewModel, Task>()
                .ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<DepartmentViewModel, Department>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<AddDepartmentViewModel, Department>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            
            CreateMap<AddEmployeeViewModel, Employee>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<AddTaskViewModel, Task>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
