using Chirp.Core;
using Chirp.Database;
using Chirp.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

        return builder;
    }
}
