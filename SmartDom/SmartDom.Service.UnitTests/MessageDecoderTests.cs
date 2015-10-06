using System;
using NUnit.Framework;
using Ploeh.AutoFixture;
using System.Linq;
using SmartDom.Service.Interface.Models;

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

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        [Description("0 is reserved device id, modbus use is for broadcasting ")]
            public void ShallThrowExceptionWhenDecodedDeviceIdIsZero()
        {
            int length = Registry.TOTAL_REGS_SIZE;
            var msg = fixture.CreateMany<ushort>(length)
                .ToArray();
            msg[Registry.ID_ADDR] = 0;
            msg[Registry.TYPE_ADDR] = (ushort)fixture.Create<DeviceType>();
            msg[Registry.SUBTYPE_ADDR] = (ushort)fixture.Create<DeviceSubtype>();
            msg[Registry.STATE_ADDR] = (ushort)fixture.Create<DeviceState>();

            classUnderTests.Decode(msg);
        }
    }
}
