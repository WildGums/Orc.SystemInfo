namespace Orc.SystemInfo.Tests;

using System.Linq;
using NUnit.Framework;

public class DbProvidersServiceFacts
{
    [TestFixture]
    public class The_GetInstalledDbProviders_Method
    {
        [Test]
        public void Returns_Non_Null_Collection()
        {
            var service = new DbProvidersService();

            var result = service.GetInstalledDbProviders();

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Returns_Results_In_Alphabetical_Order()
        {
            var service = new DbProvidersService();

            var result = service.GetInstalledDbProviders().ToList();
            var sorted = result.OrderBy(x => x).ToList();

            Assert.That(result, Is.EqualTo(sorted));
        }
    }
}
