using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Northwind.Api.Controllers;
using Northwind.Application.Abstractions.Contracts;
using Northwind.Application.Abstractions.DTOs;

namespace Northwind.Api.Tests;

public class CustomersControllerTests
{
    private static Mock<ICustomerService> MockService() => new(MockBehavior.Strict);

    private static CustomersController Sut(Mock<ICustomerService> mock) => new(mock.Object);

    [Fact]
    public async Task List_returns_Ok()
    {
        var m = MockService();

        m.Setup(x => x.BrowseAsync(1, 20, null, default)).ReturnsAsync(new List<CustomerListItemDto>());
        
        var c = Sut(m);

        var r = await c.Get();

        r.Result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task List_bad_page_returns_BadRequest()
    {
        var c = Sut(MockService());

        var r = await c.Get(page: 0);

        r.Result.Should().BeOfType<BadRequestResult>();
    }

    [Fact]
    public async Task Size_over_100_returns_BadRequest()
    {
        var r = await Sut(MockService()).Get(size: 999);

        r.Result.Should().BeOfType<BadRequestResult>();
    }
}
