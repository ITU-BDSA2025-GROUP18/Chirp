
using Chirp.Repositories;
using Chirp.Database;
using Chirp.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Chirp.Web;

public abstract class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Load database connection via configuration
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
        builder.Services.AddDbContext<ChirpDBContext>(options =>
                options.UseSqlite(connectionString, b => b.MigrationsAssembly("Chirp.Web")));

        // Add ASP.NET Core Identity to the container. Source: course book, ch. 23.4
        builder.Services.AddDefaultIdentity<Author>(options =>
                options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ChirpDBContext>();

        // Add OAuth authentication via GitHub to the container. Source: README_PROJECT, session 8
        builder.Services.AddAuthentication(options =>
            {
                /*options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;*/

                // The above code was included in the session 8 README_PROJECT OAuth example, however
                // the app only works properly if it is commented out. Do not know if this poses any
                // risks to the site... Source: https://github.com/aspnet-contrib/AspNet.Security.OAuth.Providers/issues/62

                options.DefaultChallengeScheme = "GitHub";
            })
            .AddCookie()
            .AddGitHub(o =>
            {
                o.ClientId = builder.Configuration["authentication:github:clientId"]!;
                o.ClientSecret = builder.Configuration["authentication:github:clientSecret"]!;
                o.CallbackPath = "/tmp/signin-github";
                // Changed from "signin-github" as to not confuse the url for an Author
                // (not sure if that is right or even needed)
            });
        builder.Services.AddSession();

        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddScoped<ICheepRepository, CheepRepository>();

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

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseSession();

        app.MapRazorPages();
        app.Run();
    }
}
