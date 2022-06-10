using CheatCode.Authentication.IdServer4.MvcApp;
using CheatCode.Authentication.IdServer4.MvcApp.Filters;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(o =>
{
    o.Filters.Add(new LogFilter());
});

builder.Services.AddCheatCodeDbContext();

builder.Services.AddCheatCodeIdentity();

builder.Services.AddCheatCodeAuthentication();

builder.Services.AddCheatCodeAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
