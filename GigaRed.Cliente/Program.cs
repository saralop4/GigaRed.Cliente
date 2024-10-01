using Blazored.LocalStorage;
using CurrieTechnologies.Razor.SweetAlert2;
using GigaRed.Cliente.Aplicacion.Servicios;
using GigaRed.Cliente.Dominio.Interfaces;
using GigaRed.Cliente.Infraestructura.Providers;
using GigaRed.Cliente.Infraestructura.Servicios;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace GigaRed.Cliente
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped<ApiAutenticacionStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<ApiAutenticacionStateProvider>());
            builder.Services.AddScoped<IAutenticacionServicio, AutenticacionServicio>();
            builder.Services.AddScoped<ITokenStorage, LocalTokenStorage>();
            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5043/") }); //url donde corre la apigateway

            builder.Services.AddAuthorizationCore();
            builder.Services.AddSweetAlert2();

            await builder.Build().RunAsync();
        }
    }
}
