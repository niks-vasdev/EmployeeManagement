using EmployeeManagement.Shared.ViewModels.Employee;
using EmployeeManagement.Shared.ViewModels.Task;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Data.Validations
{
    public class TaskValidator : AbstractValidator<AddTaskViewModel>
    {
        public TaskValidator()
        {
            RuleSet("Add", () =>
            {
                RuleFor(x => x.Name).NotNull().WithMessage("Name must not be null").NotEmpty().WithMessage("Name must not be empty");
                RuleFor(x => x.Title).NotNull().WithMessage("Title must not be null").NotEmpty().WithMessage("Title must not be empty");
                RuleFor(x => x.DueDate).NotNull().WithMessage("Due Date must not be null");
                RuleFor(x => x.EmployeeId).NotNull().WithMessage("Employee Id must not be null").NotEmpty().WithMessage("Employee Id must not be empty");
                RuleFor(x => x.Description).NotNull().WithMessage("Description must not be null").NotEmpty().WithMessage("Description must not be null");
            });

            RuleSet("Update", () =>
            {
                RuleFor(x => x.Id).NotNull().WithMessage("Id must not be null").NotEmpty().WithMessage("Id Name must not be empty");
                RuleFor(x => x.Name).NotNull().WithMessage("Name must not be null").NotEmpty().WithMessage("Name must not be empty");
                RuleFor(x => x.Title).NotNull().WithMessage("Title must not be null").NotEmpty().WithMessage("Title must not be empty");
                RuleFor(x => x.DueDate).NotNull().WithMessage("Due Date must not be null");
                RuleFor(x => x.EmployeeId).NotNull().WithMessage("Employee Id must not be null").NotEmpty().WithMessage("Employee Id must not be empty");
                RuleFor(x => x.Description).NotNull().WithMessage("Description must not be null").NotEmpty().WithMessage("Description must not be null");
            });
        }
    }
}
