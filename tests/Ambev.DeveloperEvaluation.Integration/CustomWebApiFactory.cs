using Microsoft.AspNetCore.Mvc.Testing;
using Ambev.DeveloperEvaluation.WebApi;
using Microsoft.AspNetCore.Hosting;

namespace Ambev.DeveloperEvaluation.Integration;

public class CustomWebApiFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test");
    }
}
