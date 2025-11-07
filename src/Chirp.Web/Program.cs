using Chirp.Services;

namespace Chirp.Web;

public abstract class Program
{
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
