using Microsoft.EntityFrameworkCore;
using BullSurvey.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register DbContext with SQLite
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3001",
        policy =>
        {
            policy.WithOrigins("https://b4a0-2603-9000-5ff0-54f0-206e-ead0-5da0-561a.ngrok-free.app","https://survey-bull.vercel.app")
                       .AllowAnyMethod()
                       .AllowAnyHeader();
        });
});


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

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
app.UseCors("AllowLocalhost3001");

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
