namespace Northwind.Application.Abstractions.DTOs;

public record CustomerDetailDto(
    string Id,
    string CompanyName,
    string ContactName,
    string Country,
    IEnumerable<OrderSummaryDto> Orders);
