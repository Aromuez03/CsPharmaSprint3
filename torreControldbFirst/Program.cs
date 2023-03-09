using Microsoft.EntityFrameworkCore;
using torreControldbFristDAL.Modelo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CspharmaInformacionalContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConnection")));

// Configure session options
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

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

// Use session middleware
app.UseSession();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapGet("/", async context =>
    {
        context.Response.Redirect("/inicioSesion");
    });
});

app.UseAuthorization();

app.MapRazorPages();

app.Run();
