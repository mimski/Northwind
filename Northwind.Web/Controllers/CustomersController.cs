using Microsoft.AspNetCore.Mvc;
using Northwind.Web.Models;
using Northwind.Web.Services;

namespace Northwind.Web.Controllers;

public sealed class CustomersController : Controller
{
    private const int PageSize = 20;
    private readonly INorthwindApiClient _apiClient;

    public CustomersController(INorthwindApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<IActionResult> Index(int page = 1, string? search = null, CancellationToken ct = default)
    {
        var customers = await _apiClient.GetCustomersAsync(page, PageSize, search, ct);

        var viewModel = new CustomerIndexViewModel(customers, page, search);

        return View(viewModel);
    }

    public async Task<IActionResult> Details(string id, CancellationToken ct)
    {
        var customer = await _apiClient.GetCustomerAsync(id, ct);

        if (customer is null)
        {
            return NotFound();
        }

        return View(new CustomerDetailsViewModel(customer));
    }
}
