using Microsoft.AspNetCore.Mvc;
using Northwind.Application.Abstractions.Contracts;
using Northwind.Application.Abstractions.DTOs;

namespace Northwind.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CustomerListItemDto>>> Get([FromQuery] int page = 1, [FromQuery] int size = 20, [FromQuery] string? search = null, CancellationToken ct = default)
    {
        if (page < 1 || size is < 1 or > 100)
        {
            return BadRequest("Invalid pagination parameters.");
        }

        var data = await _customerService.BrowseAsync(page, size, search, ct);

        return Ok(data);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerDetailDto>> Get(string id, CancellationToken ct)
    {
        var customer = await _customerService.GetAsync(id, ct);

        return customer is null ? NotFound() : Ok(customer);
    }

    [HttpGet("{id}/orders")]
    public async Task<ActionResult<IEnumerable<OrderSummaryDto>>> GetOrders(string id, CancellationToken ct)
    {
        var customer = await _customerService.GetAsync(id, ct);

        return customer is null ? NotFound() : Ok(customer.Orders);
    }
}