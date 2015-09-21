using System;
using NUnit.Framework;
using Ploeh.AutoFixture;
using System.Linq;

namespace SmartDom.Service.UnitTests
{
    [TestFixture]
    public class MessageDecoderTests
    {
        private IFixture fixture;
        private IMessageDecoder classUnderTests;

        [SetUp]
        public void Init()
        {
            fixture = new Fixture();
            classUnderTests = fixture.Create<MessageDecoder>();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShallThrowExceptionWhenMessageToDecodeIsNull()
        {
            classUnderTests.Decode(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ShallThrowExceptionWhenMessageToDecodeIsTooShort()
        {
            int length = new Random()
                .Next(0, Registry.TOTAL_REGS_SIZE); 
            var data = fixture.CreateMany<ushort>(length)
                .ToArray();
            classUnderTests.Decode(data);
        }
    }
}
