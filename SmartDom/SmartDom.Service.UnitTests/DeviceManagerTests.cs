using System;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;
using SmartDom.Service.DeviceLayers;

namespace SmartDom.Service.UnitTests
{
    [TestFixture]
    public class DeviceManagerTests
    {
        private IFixture fixture;
        private IDeviceManager cut;
        private IDeviceAccessLayer deviceAccessLayerMock;
        private IMessageDecoder messageDecoderMock;

        [SetUp]
        public void Init()
        {
            fixture = new Fixture()
                .Customize(new AutoConfiguredNSubstituteCustomization());

            deviceAccessLayerMock = fixture.Freeze<IDeviceAccessLayer>();
            messageDecoderMock = fixture.Freeze<IMessageDecoder>();
            
            cut = fixture.Create<DeviceManager>();
        }

        [Test]
        public void ShallInvokeDalWithGivenDeviceId()
        {
            //arrange
            var deviceId = fixture.Create<byte>();
           
            //act
            cut.GetDeviceAsync(deviceId);

            //assert
            deviceAccessLayerMock.Received()
                .ReadFromDeviceAsync(deviceId, Registry.ID_ADDR,
                Registry.TOTAL_REGS_SIZE);
        }

        [Test]
        public void ShallInvokeMessageDecoderWithDataReceivedFromDal()
        {
            var deviceId = fixture.Create<byte>();
            var receivedData = fixture.CreateMany(Registry.TOTAL_REGS_SIZE)
                .ToArray();

            deviceAccessLayerMock.ReadFromDeviceAsync(deviceId, Registry.ID_ADDR,
                Registry.TOTAL_REGS_SIZE).Returns(receivedData);

            //act
            cut.GetDeviceAsync(deviceId);

            //assert 
            messageDecoderMock.Received(1)
                .Decode(receivedData);
        }

        [Test]
        public void ShallNotInvokeMessageDecoderWhenReceivedDataIsNull()
        {
            var deviceId = fixture.Create<byte>();

            var receivedData = fixture.CreateMany<ushort>(Registry.TOTAL_REGS_SIZE).ToArray();

            deviceAccessLayerMock.ReadFromDeviceAsync(deviceId, Arg.Any<byte>(),
               Arg.Any<byte>()).Returns(receivedData);

            //act
            cut.GetDeviceAsync(deviceId);

            //assert 
            messageDecoderMock.Received(1).Decode(Arg.Any<ushort[]>());    
        }

        [Test]
        public void ShallNotInvokeMessageDecoderWhenReceivedDataHasIncorrectLength()
        {
            var deviceId = fixture.Create<byte>();
            var rnd = new Random();
            var dataLenth = rnd.Next(ushort.MinValue, Registry.TOTAL_REGS_SIZE);
            var receivedData = fixture.CreateMany<ushort>(dataLenth).ToArray();


            deviceAccessLayerMock.ReadFromDeviceAsync(Arg.Any<byte>(), Arg.Any<byte>(),
               Arg.Any<byte>()).Returns(receivedData);

            //act
            cut.GetDeviceAsync(deviceId);

            //assert 
            messageDecoderMock.Received(1).Decode(Arg.Any<ushort[]>());
        }

    }
}
