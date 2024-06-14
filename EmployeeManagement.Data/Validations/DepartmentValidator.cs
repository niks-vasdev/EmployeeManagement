using EmployeeManagement.Shared.Constants;
using EmployeeManagement.Shared.ViewModels.Department;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Data.Validations
{
    public class DepartmentValidator : AbstractValidator<AddDepartmentViewModel>
    {
        public DepartmentValidator()
        {
            RuleSet("Add", () =>
            {
                RuleFor(x => x.Name).NotNull().WithMessage("Name must not be null").NotEmpty().WithMessage("Name must not be empty");
                RuleFor(x => x.Description).NotNull().WithMessage("Description must not be null").NotEmpty().WithMessage("Description must not be null");
            });

            RuleSet("Update", () =>
            {
                RuleFor(x => x.Id).NotNull().WithMessage("Id must not be null").NotEmpty().WithMessage("Id must not be empty");
                RuleFor(x => x.Name).NotNull().WithMessage("Name must not be null").NotEmpty().WithMessage("Name must not be empty");
                RuleFor(x => x.Description).NotNull().WithMessage("Description must not be null").NotEmpty().WithMessage("Description must not be null");
            });
        }
    }
}
