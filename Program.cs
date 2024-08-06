using Microsoft.EntityFrameworkCore;
using WebReverseEngineering.Models; // Use o namespace correto

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure usuariosContext
builder.Services.AddDbContext<usuariosContext>(options =>
    options.UseMySql("server=localhost;database=usuarios;user=root",
        new MariaDbServerVersion(new Version(10, 4, 32))));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
