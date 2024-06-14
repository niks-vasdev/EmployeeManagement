using EmployeeManagement.API.Mapper;
using EmployeeManagement.Data.DataContext;
using EmployeeManagement.Data.Validations;
using EmployeeManagement.Service.Repository;
using EmployeeManagement.Service.Services.DepartmentService;
using EmployeeManagement.Service.Services.EmployeeService;
using EmployeeManagement.Service.Services.TaskService;
using EmployeeManagement.Shared.Filters;
using EmployeeManagement.Shared.Response;
using EmployeeManagement.Shared.ViewModels.Department;
using EmployeeManagement.Shared.ViewModels.Employee;
using EmployeeManagement.Shared.ViewModels.Task;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(option =>
{
    option.Filters.Add<BaseResponseFilter>();
});
builder.Services.AddDbContext<ApplicationDbContext>(item => item.UseSqlServer(builder.Configuration.GetConnectionString("ManagementDb")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IValidator<AddDepartmentViewModel>, DepartmentValidator>();
builder.Services.AddScoped<IValidator<AddEmployeeViewModel>, EmployeeValidator>();
builder.Services.AddScoped<IValidator<AddTaskViewModel>, TaskValidator>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
