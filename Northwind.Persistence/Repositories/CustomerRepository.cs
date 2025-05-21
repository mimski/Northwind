using Microsoft.EntityFrameworkCore;
using Northwind.Application.Abstractions.Contracts;
using Northwind.Application.Abstractions.DTOs;

namespace Northwind.Persistence.Repositories;

public sealed class CustomerRepository : ICustomerRepository
{
    private readonly NorthwindDbContext _dbContext;

    public CustomerRepository(NorthwindDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<CustomerListItemDto>> GetCustomersAsync(int page, int size, string? search, CancellationToken ct = default)
    {
        var query = _dbContext.Customers.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(c => EF.Functions.Like(c.CompanyName, $"%{search}%"));
        }

        return await query.OrderBy(c => c.CompanyName)
                          .Select(c => new CustomerListItemDto(c.CustomerId, c.CompanyName, c.Orders.Count))
                          .Skip((page - 1) * size)
                          .Take(size)
                          .ToListAsync(ct);
    }


    public async Task<CustomerDetailDto?> GetCustomerWithOrdersAsync(string id, CancellationToken ct = default)
    {
        return await _dbContext.Customers.AsNoTracking()
            .Where(c => c.CustomerId == id)
            .Select(c => new CustomerDetailDto(c.CustomerId, c.CompanyName, c.ContactName, c.Country, c.Orders.OrderByDescending(o => o.OrderDate)
                .Select(o => new OrderSummaryDto(
                    o.OrderId,
                    o.OrderDate,
                    o.OrderDetails.Sum(d => d.UnitPrice * d.Quantity * (1 - (decimal)d.Discount)),
                    o.OrderDetails.Count,
                    o.OrderDetails.Any(d => d.Product.Discontinued),
                    o.OrderDetails.Any(d => d.Product.UnitsInStock < d.Quantity)
                ))
            .ToList()
            ))
            .FirstOrDefaultAsync(ct);
    }
}
