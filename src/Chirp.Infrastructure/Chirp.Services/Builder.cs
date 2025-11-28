using Chirp.Core;
using Chirp.Database;
using Chirp.Repositories.AuthorRepository;
using Chirp.Repositories.CheepRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;

namespace Chirp.Services;

public abstract class Builder
{
    public static WebApplicationBuilder Initialize(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Load database connection via configuration
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
        builder.Services.AddDbContext<ChirpDBContext>(options =>
            options.UseSqlite(connectionString, b => b.MigrationsAssembly("Chirp.Web")));

        // Add ASP.NET Core Identity to the container. Source: course book, ch. 23.4
        builder.Services.AddDefaultIdentity<Author>(options =>
                options.SignIn.RequireConfirmedAccount = true)
            .AddSignInManager<EmailSignInManager>()
            .AddEntityFrameworkStores<ChirpDBContext>();

        // Add OAuth authentication via GitHub to the container. Source: README_PROJECT, session 8
        builder.Configuration.AddUserSecrets<Program>();
        builder.Services.AddAuthentication()
            .AddCookie()
            .AddGitHub(o =>
            {
                o.ClientId = builder.Configuration["authentication_github_clientId"]!;
                o.ClientSecret = builder.Configuration["authentication_github_clientSecret"]!;
                o.CallbackPath = "/signin-github";
            });
        builder.Services.AddSession();

        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddScoped<ICheepRepository, CheepRepository>();
        builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();

        return builder;
    }

    public static void Run(WebApplication app)
    {
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
