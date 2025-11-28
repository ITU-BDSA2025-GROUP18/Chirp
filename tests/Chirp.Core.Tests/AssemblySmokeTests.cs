using System.Reflection;
using Xunit;

namespace Chirp.Core.Tests;

public class AssemblySmokeTests
{
    [Fact]
    public void Core_Assembly_Can_Load()
    {
        var asm = Assembly.Load("Chirp.Core");
        Assert.NotNull(asm);
    }
}
