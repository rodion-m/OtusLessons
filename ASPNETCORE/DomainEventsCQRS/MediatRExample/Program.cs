using MediatRExample.Application.Behaviors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddMediatR(cfg =>
{
    // Регистрация всех обработчиков (команд и запросов) из текущей сборки
    cfg.RegisterServicesFromAssemblyContaining<Program>();
    
    // Регистрация поведения логирования
    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
