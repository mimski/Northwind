using Microsoft.Extensions.Options;
using Northwind.Web.Configuration;
using Northwind.Web.Services;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<ApiOptions>(
    builder.Configuration.GetSection(nameof(ApiOptions)));

builder.Services.AddHttpClient<INorthwindApiClient, NorthwindApiClient>(
    (sp, client) =>
    {
        var opts = sp.GetRequiredService<IOptions<ApiOptions>>().Value;
        client.BaseAddress = new Uri(opts.BaseUrl);
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
    });

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

//app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
