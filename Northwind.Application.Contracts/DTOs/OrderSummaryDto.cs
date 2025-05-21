namespace Northwind.Application.Abstractions.DTOs;

public record OrderSummaryDto(
    int OrderId,
    DateTime? OrderDate,
    decimal Total,
    int ProductsCount,
    bool HasDiscontinuedProduct,
    bool HasInsufficientStock);
