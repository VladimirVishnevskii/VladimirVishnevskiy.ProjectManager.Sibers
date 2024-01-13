using VladimirVishnevskiy.ProjectManager.Data;
using VladimirVishnevskiy.ProjectManager.Services;
using VladimirVishnevskiy.ProjectManager.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEntityFrameworkSqlite().AddDbContext<ApplicationDbContext>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
