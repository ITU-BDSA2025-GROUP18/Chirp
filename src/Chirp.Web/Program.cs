using Chirp.Services;

namespace Chirp.Web;

/// <summary>
/// The main entry point for the Chirp web application.
/// </summary>
/// <remarks>
/// This abstract <c>Program</c> class contains the <c>Main</c> method,
/// which initializes, builds, and runs the Chirp web application using
/// the <see cref="Builder"/> class.
/// </remarks>
public abstract class Program
{
    /// <summary>
    /// The main method that serves as the entry point for the application.
    /// </summary>
    /// <param name="args">Command-line arguments passed to the application.</param>
    /// <remarks>
    /// This method uses the builder pattern:
    /// <list type="bullet">
    /// <item>Initializes the <see cref="WebApplicationBuilder"/> using <see cref="Builder.Initialize"/>.</item>
    /// <item>Builds the <see cref="WebApplication"/>.</item>
    /// <item>Runs the application using <see cref="Builder.Run"/>.</item>
    /// </list>
    /// </remarks>
    public static void Main(string[] args)
    {
        // Configure the project with builder pattern
        var builder = Builder.Initialize(args);

        // Build the application
        var app = builder.Build();

        // Run the application
        Builder.Run(app);
    }
}
