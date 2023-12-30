using DataAnnotationsExample.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace DataAnnotationsExample.Controllers;

[Route("account")]
[ApiController] //запускает валидацию модели автоматически
public class AccountController : ControllerBase 
{
    [HttpPost("register")]
    public IActionResult Register(RegisterAccountRequest request)
    {
        /*
           Код ниже нужен только если атрибут [ApiController] не указан над классом контроллера.
            if (!ModelState.IsValid)
            {
                return BadRequest(
                    new ValidationProblemDetails(ModelState));
            }
         */
        
        // registration logic...
        return Ok();
    }
}