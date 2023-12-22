using FluentValidation;
using ValidationLesson.Models;
using ValidationLesson.Validators;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IValidator<Account>, AccountValidator>();
builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapControllers();

app.Run();