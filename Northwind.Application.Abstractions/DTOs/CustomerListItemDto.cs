namespace Northwind.Application.Abstractions.DTOs;

public record CustomerListItemDto(
    string Id,
    string CompanyName,
    int OrdersCount);
