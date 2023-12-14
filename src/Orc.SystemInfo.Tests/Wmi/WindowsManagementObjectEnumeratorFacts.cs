namespace Orc.SystemInfo.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Moq;
    using NUnit.Framework;
    using Orc.SystemInfo.Win32;
    using Orc.SystemInfo.Wmi;

    public class WindowsManagementObjectEnumeratorFacts
    {
        [Test]
        public async Task Returns_False_When_Enumerator_Returns_Null_Async()
        {
            IWbemClassObject objects;
            uint returnedCount;

            var wbemClassObjectEnumeratorMock = new Mock<IWbemClassObjectEnumerator>();

            wbemClassObjectEnumeratorMock.Setup(x => x.Next(It.IsAny<int>(), It.IsAny<uint>(), out objects, out returnedCount))
                .Returns((int to, uint c, out IWbemClassObject o, out uint rc) =>
                {
                    o = null;
                    rc = 0;

                    return new HResult(0);
                });

            using var enumerator = new WindowsManagementObjectEnumerator(wbemClassObjectEnumeratorMock.Object);

            var moveNextResult = enumerator.MoveNext();

            Assert.That(moveNextResult, Is.False);
        }
    }
}
