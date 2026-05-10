namespace Orc.SystemInfo.Tests;

using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Orc.SystemInfo.Wmi;

public class WindowsManagementObjectEnumeratorFacts
{
    [Test]
    [Platform(Include = "Win")]
    public void Returns_False_When_No_More_Objects()
    {
        using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        var logger = loggerFactory.CreateLogger<WindowsManagementConnection>();

        using var connection = new WindowsManagementConnection(logger);
        using var enumerator = connection.ExecuteQuery(connection.CreateQuery("SELECT * FROM Win32_OperatingSystem"));

        // Win32_OperatingSystem always has exactly one instance
        Assert.That(enumerator.MoveNext(), Is.True);
        Assert.That(enumerator.MoveNext(), Is.False);
    }
}
