using ClientApi.Components;
using ClientApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddSingleton<TokenModel>();


// Add the httpclient to the container with url from appsettings.json
builder.Services.AddHttpClient("api")
    .ConfigureHttpClient(client =>
    {
        client.BaseAddress = new Uri(builder.Configuration["ApiUrl"]!);
	});




//builder.Services.AddHttpClient("api", opts =>
//{
//    opts.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ApiUrl"));
//});



var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
   
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
