using MyFitnessBud.Data;
using MyFitnessBud.Services;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Determine which database to use
var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");
var isProduction = !string.IsNullOrEmpty(connectionString);

if (isProduction)
{
    // Render: Use PostgreSQL from DATABASE_URL environment variable
    var npgsqlBuilder = new NpgsqlConnectionStringBuilder(connectionString);
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(npgsqlBuilder.ConnectionString));
}
else
{
    // Local development: Use SQLite
    var defaultConnection = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlite(defaultConnection));
}

builder.Services.AddHttpClient<OpenFoodFactsClient>(client =>
{
    client.BaseAddress = new Uri("https://world.openfoodfacts.org/");
    client.DefaultRequestHeaders.UserAgent.ParseAdd("MyFitnessBud/1.0");
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Optional for local testing if redirect causes trouble:
// app.UseHttpsRedirection();

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors("FrontendPolicy");

app.UseAuthorization();

app.UseDefaultFiles();
app.UseStaticFiles();
app.MapStaticAssets();
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
