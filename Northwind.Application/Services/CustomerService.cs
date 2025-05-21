using Northwind.Application.Abstractions.Contracts;
using Northwind.Application.Abstractions.DTOs;

namespace Northwind.Application.Services;

public sealed class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public Task<IReadOnlyList<CustomerListItemDto>> BrowseAsync(int page, int size, string? search, CancellationToken ct) =>
        _customerRepository.GetCustomersAsync(page, size, search, ct);

    public Task<CustomerDetailDto?> GetAsync(string id, CancellationToken ct) =>
        _customerRepository.GetCustomerWithOrdersAsync(id, ct);
}
