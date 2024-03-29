using Api.Data;
using Api.Middlewares;
using Api.Repositories;
using Api.Services;
using Microsoft.OpenApi.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Add automapper DI
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Employee Benefit Cost Calculation Api",
        Description = "Api to support employee benefit cost calculations"
    });
});

// Add Scoped Services and Repositories
builder.Services.AddScoped<IPaycheckService, PaycheckService>();
builder.Services.AddScoped<IPaycheckCalculationService, PaycheckCalculationService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IDependentRepository, DependentRepository>();

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>();

// Ensure creation of In Memory DB
using (var context = new ApplicationDbContext())
{
    context.Database.EnsureCreated();
}

var allowLocalhost = "allow localhost";
builder.Services.AddCors(options =>
{
    options.AddPolicy(allowLocalhost,
        policy => policy.WithOrigins("http://localhost:3000", "http://localhost"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add middlewar for error response handling
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCors(allowLocalhost);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
