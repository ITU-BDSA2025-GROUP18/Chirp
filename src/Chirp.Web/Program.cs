using Chirp.Repositories;
using Chirp.Database;
using Chirp.Core;
using Chirp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Chirp.Web;

public abstract class Program
{
    public static void Main(string[] args)
    {
        // Configure the project with builder pattern
        var builder = Builder.Initialize(args);

        // Build the application
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
