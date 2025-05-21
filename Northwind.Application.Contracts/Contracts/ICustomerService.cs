using Northwind.Application.Abstractions.DTOs;

namespace Northwind.Application.Abstractions.Contracts;

public interface ICustomerService
{
    Task<IReadOnlyList<CustomerListItemDto>> BrowseAsync(int page, int size, string? search, CancellationToken ct);

    Task<CustomerDetailDto?> GetAsync(string id, CancellationToken ct);
}
