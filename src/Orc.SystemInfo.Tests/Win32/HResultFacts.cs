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
}
