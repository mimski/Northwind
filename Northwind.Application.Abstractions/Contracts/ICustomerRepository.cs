using Northwind.Application.Abstractions.DTOs;

namespace Northwind.Application.Abstractions.Contracts;

public interface ICustomerRepository
{
    Task<IReadOnlyList<CustomerListItemDto>> GetCustomersAsync(int pageNumber, int pageSize, string? search, CancellationToken ct = default);

    Task<CustomerDetailDto?> GetCustomerWithOrdersAsync(string customerId, CancellationToken ct = default);
}
