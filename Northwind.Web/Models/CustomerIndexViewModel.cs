using Northwind.Application.Abstractions.DTOs;

namespace Northwind.Web.Models;

public record CustomerIndexViewModel(IReadOnlyList<CustomerListItemDto> Customers, int Page, string? Search);
