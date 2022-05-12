using Microsoft.AspNetCore.Mvc;
using codetest.Services;

namespace codetest.Controllers;
// Controller for supplying the cheese list

[ApiController]
[Route("/api")]
public class CheeseController : ControllerBase
{
    private readonly ILogger<CheeseController> _logger;
    private readonly ICheeseyDataService _data;
    public CheeseController(ILogger<CheeseController> logger, ICheeseyDataService data)
    {
        _logger = logger;
        _data = data;
    }

    [HttpGet]
    [Route("/api/cheese")]
    public async Task<IEnumerable<Cheese>> Get()
    {
        return await _data.ListProducts();
    }
}