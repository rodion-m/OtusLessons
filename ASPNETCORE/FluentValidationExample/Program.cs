using FluentValidation;
using ValidationLesson.Validators;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using ValidationLesson.Models.Requests;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IValidator<RegisterAccountRequest>, RegisterAccountRequestValidator>();

builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "App is working.");
app.MapControllers();

app.Run();