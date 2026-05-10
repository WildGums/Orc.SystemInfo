namespace Orc.SystemInfo.Tests.Win32;

using System.Runtime.InteropServices;
using NUnit.Framework;
using Orc.SystemInfo.Win32;

public class HResultFacts
{
    [TestFixture]
    public class The_ThrowIfFailed_Method
    {
        [Test]
        public void Does_Throw_If_Failed()
        {
            var hresult = new HResult(-1);

            Assert.That(hresult.Failed, Is.True);
            Assert.Throws<COMException>(() => hresult.ThrowIfFailed());
        }

        [Test]
        public void Does_Not_Throw_If_Not_Failed()
        {
            var hresult = new HResult(0);

            Assert.That(hresult.Failed, Is.False);
            Assert.DoesNotThrow(() => hresult.ThrowIfFailed());
        }
    }

    [TestFixture]
    public class The_ToString_Method
    {
        [Test]
        public void Returns_Formatted_String_With_Value_And_Failed_Status_When_Failed()
        {
            var hresult = new HResult(-1);

            var result = hresult.ToString();

            Assert.That(result, Is.EqualTo("-1 (Failed = True)"));
        }

        [Test]
        public void Returns_Formatted_String_With_Value_And_Failed_Status_When_Succeeded()
        {
            var hresult = new HResult(0);

            var result = hresult.ToString();

            Assert.That(result, Is.EqualTo("0 (Failed = False)"));
        }
    }

    [TestFixture]
    public class The_Equals_Method
    {
        [Test]
        public void Returns_True_For_Equal_Values()
        {
            var hresult1 = new HResult(42);
            var hresult2 = new HResult(42);

            Assert.That(hresult1.Equals(hresult2), Is.True);
        }

        [Test]
        public void Returns_False_For_Different_Values()
        {
            var hresult1 = new HResult(0);
            var hresult2 = new HResult(-1);

            Assert.That(hresult1.Equals(hresult2), Is.False);
        }
    }
}
