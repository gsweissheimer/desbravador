using Microsoft.EntityFrameworkCore;
using v1.BLL;
using v1.Data;
using v1.Repositories;
using v1.Repository;
using v1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Repo
builder.Services.AddScoped<IRepository, Repository>();
//BLL
builder.Services.AddScoped<IProjectBLL, ProjectBLL>();
builder.Services.AddScoped<IUserRecordBLL, UserRecordBLL>();
builder.Services.AddScoped<IProjectUserRecordBLL, ProjectUserRecordBLL>();

builder.Services.AddScoped<IRandomUserBLL, RandomUserBLL>();
//DAO
builder.Services.AddScoped<IProjectDAO, ProjectDAO>();
builder.Services.AddScoped<IUserRecordDAO, UserRecordDAO>();
builder.Services.AddScoped<IProjectUserRecordDAO, ProjectUserRecordDAO>();

builder.Services.AddScoped<IErrorLogRepository, ErrorLogRepository>();
builder.Services.AddScoped<IErrorLogService, ErrorLogService>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
string MyAllowSpecificOrigins = "AllowAllOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy( MyAllowSpecificOrigins,
                builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
});

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

app.UseCors(MyAllowSpecificOrigins);

app.Run();
