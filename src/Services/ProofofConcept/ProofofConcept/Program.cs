using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using ProofofConcept;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://host.docker.internal:8080/") });

builder.Services.AddSingleton(sp =>
{
    return new HubConnectionBuilder()
        .WithUrl("http://host.docker.internal:8080/ratehub")
        .WithAutomaticReconnect()
        .Build();
});

await builder.Build().RunAsync();
