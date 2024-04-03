using MediatR;
using MediatRExample.Application.Commands;
using MediatRExample.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MediatRExample.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] AddUserCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUser(int userId)
    {
        var user = await _mediator.Send(new GetUserQuery { UserId = userId });
        return Ok(user);
    }
}
