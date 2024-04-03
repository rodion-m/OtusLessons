using MediatR;

namespace MediatRExample.Application.Commands.Handlers;

public class AddUserCommandHandler : IRequestHandler<AddUserCommand, bool>
{
    public Task<bool> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        // Здесь должен быть код для добавления пользователя в базу данных.
        // Возвращаем true для простоты примера.
        return Task.FromResult(true);
    }
}
