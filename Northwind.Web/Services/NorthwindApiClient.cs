using Microsoft.Extensions.Options;
using Northwind.Application.Abstractions.DTOs;
using Northwind.Web.Configuration;
using System.Net;
using System.Net.Http.Headers;

namespace Northwind.Web.Services;

public sealed class NorthwindApiClient : INorthwindApiClient
{
    private readonly HttpClient _httpClient;

    public NorthwindApiClient(HttpClient httpClient, IOptions<ApiOptions> apiOptions)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(apiOptions.Value.BaseUrl);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<IReadOnlyList<CustomerListItemDto>> GetCustomersAsync(int page, int size, string? search, CancellationToken ct = default)
    {
        var url = $"customers?page={page}&size={size}&search={Uri.EscapeDataString(search ?? "")}";

        return await _httpClient.GetFromJsonAsync<IReadOnlyList<CustomerListItemDto>>(url, ct) ?? Array.Empty<CustomerListItemDto>();
    }

    public async Task<CustomerDetailDto?> GetCustomerAsync(string id, CancellationToken ct = default)
    {
        var respons = await _httpClient.GetAsync($"customers/{id}", ct);

        if (respons.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        respons.EnsureSuccessStatusCode();

        return await respons.Content.ReadFromJsonAsync<CustomerDetailDto>(cancellationToken: ct);
    }
}
