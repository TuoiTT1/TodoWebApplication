using System.Data;
using System.Reflection;
using Dapper;
using Microsoft.Data.SqlClient;
using TodoWebApplication.Domain.Interfaces;
using TodoWebApplication.Infrastructure.Persistence;
using TodoWebApplication.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database Connection
builder.Services.AddScoped<IDbConnection>(_ =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

// Cấu hình logging
builder.Logging.ClearProviders();           // Xóa các provider mặc định nếu không cần
builder.Logging.AddConsole();               // Ghi log ra console
builder.Logging.AddDebug();                 // Ghi log vào Debug Output (VS)
//builder.Logging.AddEventLog();              // Ghi log vào Windows Event Log (nếu cần)


// add MediatR services
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();


// Đăng ký Dapper TypeHandler trong dịch vụ ứng dụng
SqlMapper.AddTypeHandler(new EmployeeLevelHandler());


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
