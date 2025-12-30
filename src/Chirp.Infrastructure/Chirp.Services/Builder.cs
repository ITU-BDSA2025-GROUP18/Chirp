using Chirp.Core;
using Chirp.Database;
using Chirp.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;

namespace Chirp.Services;

/// <summary>
/// Provides helper methods to initialize and configure the Chirp web application.
/// </summary>
/// <remarks>
/// This abstract class contains static methods to create and configure
/// the <see cref="WebApplicationBuilder"/> and <see cref="WebApplication"/> instances
/// for the Chirp project. It sets up the database context, Identity authentication,
/// OAuth authentication via GitHub, dependency injection for repositories, and
/// middleware for request handling.
/// </remarks>
public abstract class Builder
{
    /// <summary>
    /// Initializes and configures a <see cref="WebApplicationBuilder"/> with
    /// database context, authentication, services, and repositories.
    /// </summary>
    /// <param name="args">Command-line arguments passed to the application.</param>
    /// <returns>A fully configured <see cref="WebApplicationBuilder"/> instance.</returns>
    /// <remarks>
    /// The method selects the database connection string based on whether
    /// the "--playwright" flag is present in the arguments.
    /// It configures ASP.NET Core Identity with the custom <see cref="EmailSignInManager"/>,
    /// OAuth authentication via GitHub, session support, Razor Pages, and
    /// scoped services for repositories (<see cref="ICheepRepository"/>,
    /// <see cref="IAuthorRepository"/>, <see cref="IFollowerRepository"/>).
    /// </remarks>
    public static WebApplicationBuilder Initialize(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Load database connection via configuration
        var connectionString = args.Contains("--playwright") ?
            builder.Configuration.GetConnectionString("PlaywrightConnection") :
            builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<ChirpDBContext>(options =>
            options.UseSqlite(connectionString, b => b.MigrationsAssembly("Chirp.Web")));

        // Add ASP.NET Core Identity
        builder.Services.AddDefaultIdentity<Author>(options =>
                options.SignIn.RequireConfirmedAccount = true)
            .AddSignInManager<EmailSignInManager>()
            .AddEntityFrameworkStores<ChirpDBContext>();

        // Add OAuth authentication via GitHub
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

        // Add Razor Pages and repository services
        builder.Services.AddRazorPages();
        builder.Services.AddScoped<ICheepRepository, CheepRepository>();
        builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
        builder.Services.AddScoped<IFollowerRepository, FollowerRepository>();

        return builder;
    }

    /// <summary>
    /// Configures and runs the <see cref="WebApplication"/> with middleware and endpoints.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> to configure and run.</param>
    /// <remarks>
    /// This method configures exception handling, HTTPS redirection, static files,
    /// routing, authentication, authorization, session middleware, and Razor Pages mapping.
    /// It then starts the application.
    /// </remarks>
    public static void Run(WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days.
            // You may want to change this for production scenarios,
            // see https://aka.ms/aspnetcore-hsts.
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
