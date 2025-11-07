using System.Diagnostics;
using Microsoft.Playwright;

namespace Chirp.Web.Tests.End_2_End_Tests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class PlaywrightEndToEndTests : PageTest
{
    private Process? _webAppProcess;
    private const string BaseUrl = "https://localhost:5001";

    [OneTimeSetUp]
    public async Task StartWebApp()
    {
        var repoRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../../.."));
        var projectPath = Path.Combine(repoRoot, "src", "Chirp.Web", "Chirp.Web.csproj");

        var psi = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = $"run --project \"{projectPath}\" --urls=https://localhost:5001",
            WorkingDirectory = repoRoot,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };


        _webAppProcess = Process.Start(psi);
        _webAppProcess!.OutputDataReceived += (_, e) => Console.WriteLine(e.Data);
        _webAppProcess!.ErrorDataReceived += (_, e) => Console.WriteLine("ERR: " + e.Data);
        _webAppProcess!.BeginOutputReadLine();
        _webAppProcess!.BeginErrorReadLine();

        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (_, _, _, _) => true
        };
        using var httpClient = new HttpClient(handler);

        var maxAttempts = 60;
        var delayMs = 500;

        for (int i = 0; i < maxAttempts; i++)
        {
            try
            {
                var response = await httpClient.GetAsync("https://localhost:5001");
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("✅ Local server is ready!");
                    return;
                }
            }
            catch
            {
                // server not up yet
            }

            await Task.Delay(delayMs);
        }

        throw new Exception("❌ Web app failed to start after waiting 30 seconds.");
    }

    [OneTimeTearDown]
    public void StopWebApp()
    {
        if (_webAppProcess is { HasExited: false })
        {
            _webAppProcess.Kill(entireProcessTree: true);
            _webAppProcess.WaitForExit();
        }

        _webAppProcess?.Dispose();
    }


    [Test]
    public async Task BasicRedirectionsTest()
    {
        await Page.GotoAsync(BaseUrl);

        // --------------- NAV BAR ---------------
        await Page.GetByRole(AriaRole.Link, new() { Name = "Register" }).ClickAsync();
        await Expect(Page).ToHaveURLAsync(new Regex("/Identity/Account/Register"));

        await Page.GetByRole(AriaRole.Link, new() { Name = "Public timeline" }).ClickAsync();
        await Expect(Page).ToHaveURLAsync("https://localhost:5001/");

        await Page.GetByRole(AriaRole.Link, new() { Name = "Login" }).ClickAsync();
        await Expect(Page).ToHaveURLAsync(new Regex("/Identity/Account/Login"));


        // --------------- CHIRP PICTURE ICON ---------------
        await Page.GetByRole(AriaRole.Img, new() { Name = "Icon1" }).ClickAsync();
        await Expect(Page).ToHaveURLAsync("https://localhost:5001/");


        // --------------- PAGINATION FOOTER ---------------
        await Page.GetByText("Next Page").ClickAsync();
        await Expect(Page).ToHaveURLAsync(new Regex("\\?page=2"));

        await Page.GetByText("Previous Page").ClickAsync();
        await Expect(Page).ToHaveURLAsync(new Regex("\\?page=1"));
    }
}
