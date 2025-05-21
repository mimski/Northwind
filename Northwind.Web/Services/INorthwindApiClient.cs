using Northwind.Application.Abstractions.DTOs;

namespace Northwind.Web.Services;

public interface INorthwindApiClient
{
    Task<IReadOnlyList<CustomerListItemDto>> GetCustomersAsync(int page, int size, string? search, CancellationToken ct = default);

    Task<CustomerDetailDto?> GetCustomerAsync(string id, CancellationToken ct = default);
}
