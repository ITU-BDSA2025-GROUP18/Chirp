using Microsoft.EntityFrameworkCore;
using Chirp.Database;
using Chirp.Repositories;

namespace Chirp.Web;

public abstract class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddScoped<ICheepQueryRepository, CheepQueryRepository>();

        // Load database connection via configuration
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
        builder.Services.AddDbContext<ChirpDBContext>(options => options.UseSqlite(connectionString));

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
        app.MapRazorPages();
        app.Run();
    }
}
