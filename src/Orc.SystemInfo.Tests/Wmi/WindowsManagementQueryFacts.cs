namespace Orc.SystemInfo.Tests;

using System;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using Orc.SystemInfo.Win32;
using Orc.SystemInfo.Wmi;

public class WindowsManagementQueryFacts
{
    [TestFixture]
    public class The_Constructor
    {
        [Test]
        public void Sets_Wql_Property()
        {
            using var connection = new WindowsManagementConnection(NullLogger<WindowsManagementConnection>.Instance);
            const string wql = "SELECT * FROM Win32_OperatingSystem";

            var query = new WindowsManagementQuery(connection, wql);

            Assert.That(query.Wql, Is.EqualTo(wql));
        }

        [Test]
        public void Sets_Connection_Property()
        {
            using var connection = new WindowsManagementConnection(NullLogger<WindowsManagementConnection>.Instance);
            const string wql = "SELECT * FROM Win32_OperatingSystem";

            var query = new WindowsManagementQuery(connection, wql);

            Assert.That(query.Connection, Is.SameAs(connection));
        }

        [Test]
        public void Sets_Default_EnumeratorBehaviorOption_To_ReturnImmediately()
        {
            using var connection = new WindowsManagementConnection(NullLogger<WindowsManagementConnection>.Instance);
            const string wql = "SELECT * FROM Win32_OperatingSystem";

            var query = new WindowsManagementQuery(connection, wql);

            Assert.That(query.EnumeratorBehaviorOption, Is.EqualTo(WbemClassObjectEnumeratorBehaviorOptions.ReturnImmediately));
        }

        [Test]
        public void Throws_ArgumentNullException_For_Null_Connection()
        {
            Assert.Throws<ArgumentNullException>(() => new WindowsManagementQuery(null!, "SELECT * FROM Win32_OperatingSystem"));
        }

        [Test]
        public void Throws_ArgumentNullException_For_Null_Wql()
        {
            using var connection = new WindowsManagementConnection(NullLogger<WindowsManagementConnection>.Instance);

            Assert.Throws<ArgumentNullException>(() => new WindowsManagementQuery(connection, null!));
        }
    }
}
