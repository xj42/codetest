using Microsoft.AspNetCore.Mvc;
using codetest.Services;

namespace codetest.Controllers;
// Controller for handeling the orders functions.
// basically REST

[ApiController]
[Route("/api")]
public class CheeseOrderController : ControllerBase
{
    private readonly ILogger<CheeseOrderController> _logger;
    private readonly ICheeseyDataService _data;
    public CheeseOrderController(ILogger<CheeseOrderController> logger, ICheeseyDataService data)
    {
        _logger = logger;
        _data = data;
    }

    [HttpGet]
    [Route("/api/cheeseorder")]
    public async Task<List<CheeseOrder>> Get()
    {
        return await _data.ListOrder();
    }

    [HttpPost]
    [Route("/api/cheeseorder")]
    public async Task<bool> Add(CheeseOrder cheese)
    {
        return await _data.AddToOrder(cheese);
    }

    // not in use in this example
    [HttpPost]
    [Route("/api/cheeseorder/update")]
    public async Task<bool> Update(CheeseOrder list)
    {
        return await _data.UpdateOrder(list);
    }

    [HttpPost]
    [Route("/api/cheeseorder/delete")]
    public async Task<bool> Delete(CheeseOrder item)
    {
        return await _data.DeleteOrder(item);
    }
}