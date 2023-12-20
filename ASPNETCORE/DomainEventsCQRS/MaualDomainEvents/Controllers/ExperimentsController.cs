using MaualDomainEvents.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MaualDomainEvents.Controllers;

[ApiController]
[Route("Experiments")]
public class ExperimentsController : ControllerBase
{
    private readonly ILogger<ExperimentsController> _logger; 

    public ExperimentsController(ILogger<ExperimentsController> logger)
    {
        _logger = logger;
    }

    [HttpGet("ForeverPrint")]
    public IActionResult ForeverPrint(
        [FromQuery] string text, CancellationToken cancellationToken)
    {
        //HttpContext.RequestAborted
        while (true)
        {
            _logger.LogInformation("{Text} DateTime: {DateTime}", text, DateTime.Now);
            //await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
        }

        return Ok(new Product());
    }
}