using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Finance.Web;
using MudBlazor.Services;
using Finance.Web.Security;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<CookieHandler>();//

builder.Services.AddMudServices();
builder.Services.AddHttpClient(Configuration.HttpClientName, opt => {
    opt.BaseAddress = new Uri(Configuration.BackendUrl);
         
}).AddHttpMessageHandler<CookieHandler>();
await builder.Build().RunAsync();
