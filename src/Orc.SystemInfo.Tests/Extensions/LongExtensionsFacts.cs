namespace Orc.SystemInfo.Tests;

using NUnit.Framework;

public class LongExtensionsFacts
{
    [TestFixture]
    public class The_ToReadableSize_Method
    {
        [Test]
        public void Returns_Bytes_For_Values_Under_1KB()
        {
            var value = 512L;

            var result = value.ToReadableSize();

            Assert.That(result, Is.EqualTo("512.00 bytes"));
        }

        [Test]
        public void Returns_KB_For_Kilobyte_Values()
        {
            var value = 1024L;

            var result = value.ToReadableSize();

            Assert.That(result, Is.EqualTo("1.00 KB"));
        }

        [Test]
        public void Returns_MB_For_Megabyte_Values()
        {
            var value = 1024L * 1024L;

            var result = value.ToReadableSize();

            Assert.That(result, Is.EqualTo("1.00 MB"));
        }

        [Test]
        public void Returns_Negative_Prefix_For_Negative_Values()
        {
            var value = -1024L;

            var result = value.ToReadableSize();

            Assert.That(result, Does.StartWith("-"));
            Assert.That(result, Does.Contain("KB"));
        }

        [Test]
        public void Ulong_Overload_Returns_Same_As_Long_Overload()
        {
            var value = 2048UL;

            var result = value.ToReadableSize();

            Assert.That(result, Is.EqualTo(((long)value).ToReadableSize()));
        }
    }
}
