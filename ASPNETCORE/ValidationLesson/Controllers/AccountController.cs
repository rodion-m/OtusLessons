using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ValidationLesson.Models;

namespace ValidationLesson.Controllers;

[Route("account")]
[ApiController]
public class AccountController : ControllerBase 
{
    private readonly IValidator<Account> _validator;

   
    public AccountController(IValidator<Account> validator) 
    {
        // Inject our validator and also a DB context for storing our person object.
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(Account account) 
    {
        return Ok();
    }
}