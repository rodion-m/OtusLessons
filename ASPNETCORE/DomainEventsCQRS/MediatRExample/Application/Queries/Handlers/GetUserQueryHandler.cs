using MediatR;
using MediatRExample.Entities;

namespace MediatRExample.Application.Queries.Handlers;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, User>
{
    public async Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        // Здесь должен быть код для получения пользователя из базы данных.
        // Возвращаем пример пользователя для наглядности.
        return await Task.FromResult(new User { UserId = request.UserId, Name = "Иван", Age = 30 });
    }
}