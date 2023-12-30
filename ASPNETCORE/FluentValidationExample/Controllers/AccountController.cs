using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ValidationLesson.Models;
using ValidationLesson.Models.Requests;

namespace ValidationLesson.Controllers;

[Route("account")]
[ApiController]
public class AccountController : ControllerBase 
{
    private readonly IValidator<RegisterAccountRequest> _validator;

   
    public AccountController(IValidator<RegisterAccountRequest> validator) 
    {
        // Внедрять валидатор необязательно, если был вызван метод Services.AddFluentValidationAutoValidation()
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterAccountRequest request)
    {
        /*
           Код ниже нужен только если метод Services.AddFluentValidationAutoValidation() не был вызван
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
         */
        
        // registration logic...
        return Ok();
    }
}