using MediatR;

namespace MediatRExample.Application.Commands;

public class AddUserCommand : IRequest<bool> // Возвращает true, если пользователь успешно добавлен.
{
    public string Name { get; set; }
    public int Age { get; set; }
}