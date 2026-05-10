namespace Orc.SystemInfo.Tests;

using NUnit.Framework;

public class SystemInfoElementFacts
{
    [TestFixture]
    public class The_Constructor
    {
        [Test]
        public void Default_Constructor_Sets_Empty_Strings()
        {
            var element = new SystemInfoElement();

            Assert.That(element.Name, Is.EqualTo(string.Empty));
            Assert.That(element.Value, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Parameterized_Constructor_Sets_Name_And_Value()
        {
            var element = new SystemInfoElement("OS", "Windows 11");

            Assert.That(element.Name, Is.EqualTo("OS"));
            Assert.That(element.Value, Is.EqualTo("Windows 11"));
        }
    }

    [TestFixture]
    public class The_ToString_Method
    {
        [Test]
        public void Returns_Name_Only_When_Value_Is_Empty()
        {
            var element = new SystemInfoElement("CPU", string.Empty);

            var result = element.ToString();

            Assert.That(result, Is.EqualTo("CPU"));
        }

        [Test]
        public void Returns_Name_And_Value_Separated_By_Colon_When_Both_Set()
        {
            var element = new SystemInfoElement("CPU", "Intel i9");

            var result = element.ToString();

            Assert.That(result, Is.EqualTo("CPU: Intel i9"));
        }

        [Test]
        public void Returns_Indented_Value_When_Name_Is_Empty()
        {
            var element = new SystemInfoElement(string.Empty, "some value");

            var result = element.ToString();

            Assert.That(result, Is.EqualTo("  some value"));
        }
    }
}
