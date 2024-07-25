using Microsoft.Extensions.Options;
using SMSystems.IoC;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton(AutoMapperConfig.Initialize());
builder.Services.AddRazorPages();
builder.Services.ConfigureServices(builder.Configuration);
builder.Services.RegisterServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Add the localization middleware

app.MapRazorPages();
app.UseRequestLocalization("pt-br");
app.Run();


