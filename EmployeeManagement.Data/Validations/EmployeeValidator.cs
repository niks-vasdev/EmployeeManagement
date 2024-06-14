using EmployeeManagement.Shared.ViewModels.Employee;
using FluentValidation;

namespace EmployeeManagement.Data.Validations
{
    public class EmployeeValidator : AbstractValidator<AddEmployeeViewModel>
    {
        public EmployeeValidator()
        {
            RuleSet("Add", () =>
            {
                RuleFor(x => x.FirstName).NotNull().WithMessage("First Name must not be null").NotEmpty().WithMessage("First Name must not be empty");
                RuleFor(x => x.LastName).NotNull().WithMessage("Last Name must not be null").NotEmpty().WithMessage("Last Name must not be empty");
                RuleFor(x => x.HireDate).NotNull().WithMessage("Hire Date must not be null");
                RuleFor(x => x.DepartmentId).NotNull().WithMessage("DepartmentId must not be null").NotEmpty().WithMessage("DepartmentId must not be empty");
                RuleFor(x => x.Phone).NotNull().WithMessage("Phone no. Name must not be null").NotEmpty().WithMessage("Phone no. Name must not be empty").Length(8,12).WithMessage("Phone no. must be 8 to 12");
                RuleFor(x => x.Email).NotNull().WithMessage("Email must not be null").NotEmpty().WithMessage("Email must not be null").EmailAddress().WithMessage("Email must be valid");
            });

            RuleSet("Update", () =>
            {
                RuleFor(x => x.Id).NotNull().WithMessage("Id must not be null").NotEmpty().WithMessage("Id Name must not be empty");
                RuleFor(x => x.FirstName).NotNull().WithMessage("First Name must not be null").NotEmpty().WithMessage("First Name must not be empty");
                RuleFor(x => x.LastName).NotNull().WithMessage("Last Name must not be null").NotEmpty().WithMessage("Last Name must not be empty");
                RuleFor(x => x.HireDate).NotNull().WithMessage("Hire Date must not be null");
                RuleFor(x => x.DepartmentId).NotNull().WithMessage("DepartmentId must not be null").NotEmpty().WithMessage("DepartmentId must not be empty");
                RuleFor(x => x.Phone).NotNull().WithMessage("Phone no. Name must not be null").NotEmpty().WithMessage("Phone no. Name must not be empty").Length(8, 12).WithMessage("Phone no. must be 8 to 12");
                RuleFor(x => x.Email).NotNull().WithMessage("Email must not be null").NotEmpty().WithMessage("Email must not be null").EmailAddress().WithMessage("Email must be valid");
            });
        }
    }
}
