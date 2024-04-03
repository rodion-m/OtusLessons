using MediatR;
using MediatRExample.Entities;

namespace MediatRExample.Application.Queries;

public class GetUserQuery : IRequest<User>
{
    public int UserId { get; set; }
}