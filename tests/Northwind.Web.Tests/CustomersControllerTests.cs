using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Northwind.Web.Controllers;
using Northwind.Web.Models;
using Northwind.Web.Services;
using Northwind.Application.Abstractions.DTOs;

namespace Northwind.Web.Tests;

public class CustomersControllerTests
{
    private static (CustomersController ctl, Mock<INorthwindApiClient> mock) Sut()
    {
        var m = new Mock<INorthwindApiClient>(MockBehavior.Strict);

        return (new CustomersController(m.Object), m);
    }

    [Fact]
    public async Task Index_returns_view()
    {
        var (ctl, m) = Sut();

        m.Setup(x => x.GetCustomersAsync(1, 20, null, default)).ReturnsAsync(Array.Empty<CustomerListItemDto>());

        var r = await ctl.Index();

        r.Should().BeOfType<ViewResult>().Which.Model.Should().BeOfType<CustomerIndexViewModel>();
    }
}
