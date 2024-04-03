using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ValidationLesson.Models.Requests;

namespace ValidationLesson.Controllers;

[Route("account")]
[ApiController]
public class AccountController(IValidator<RegisterAccountRequest> validator) : ControllerBase
{
    [HttpPost("register")]
    public IActionResult Register(RegisterAccountRequest request)
    {
        // Код ниже нужен только если метод Services.AddFluentValidationAutoValidation() не был вызван
        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        
        // registration logic...
        return Ok();
    }
}