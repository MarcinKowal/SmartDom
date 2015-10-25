// SmartDom
// SmartDom.Service.UnitTests
// DeviceManagerTests.cs
//  
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
//  

namespace SmartDom.Service.UnitTests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using NSubstitute;
    using NSubstitute.ExceptionExtensions;

    using NUnit.Framework;

    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoNSubstitute;

   
    using SmartDom.Service.DeviceLayers;
    using SmartDom.Service.Interface.Models;

    [TestFixture]
    public class DeviceManagerTests
    {
        [SetUp]
        public void Init()
        {
            this.fixture = new Fixture().Customize(new AutoConfiguredNSubstituteCustomization());

            this.deviceAccessLayerMock = this.fixture.Freeze<IDeviceAccessLayer>();
            this.messageDecoderMock = this.fixture.Freeze<IMessageDecoder>();
      
            this.cut = this.fixture.Create<DeviceManager>();
        }

        private IFixture fixture;

        private IDeviceManager cut;

        private IDeviceAccessLayer deviceAccessLayerMock;

        private IMessageDecoder messageDecoderMock;

      
        [Test]
        public async Task ShallInvokeMessageDecoderWhenGetDevice()
        {
            var deviceId = this.fixture.Create<byte>();
            var receivedData = this.fixture.CreateMany(Registry.TOTAL_REGS_SIZE).ToArray();

            this.deviceAccessLayerMock.ReadFromDeviceAsync(deviceId, Registry.ID_ADDR, Registry.TOTAL_REGS_SIZE)
                .Returns(Task.FromResult(receivedData));

            //act
            await this.cut.GetDeviceAsync(deviceId);

            //assert 
            this.messageDecoderMock.Received(1).Decode(receivedData);
        }

        [Test]
        [ExpectedException(typeof(DeviceException))]
        public async Task ShallThrowDeviceExceptionWhenExceptionOccuredFromDecoder()
        {
            var exception = this.fixture.Create<Exception>();
            var deviceId = this.fixture.Create<byte>();
            var receivedData = this.fixture.CreateMany(Registry.TOTAL_REGS_SIZE).ToArray();

            this.messageDecoderMock.Decode(receivedData).ThrowsForAnyArgs(exception);

            //act
            await this.cut.GetDeviceAsync(deviceId);
        }

        [Test]
        [ExpectedException(typeof(DeviceException))]
        public async Task ShallThrowDeviceExceptionWhenExceptionOccuredFromDeviceLayer()
        {
            //assert
            var exception = this.fixture.Create<Exception>();
            var deviceId = this.fixture.Create<byte>();

            this.deviceAccessLayerMock.ReadFromDeviceAsync(deviceId, Registry.ID_ADDR, Registry.TOTAL_REGS_SIZE)
                .ThrowsForAnyArgs(exception);

            //act
            await this.cut.GetDeviceAsync(deviceId);
        }

        [Test]
        [ExpectedException(typeof(DeviceException))]
        public async Task ShallThrowExceptionWhenWritesToDeviceLayer()
        {
            //assert
            var deviceId = this.fixture.Create<byte>();
            var sentData = this.fixture.CreateMany<ushort>().ToArray();
            var deviceState = this.fixture.Create<DeviceState>();
            var address = this.fixture.Create<ushort>();
            var exception = this.fixture.Create<Exception>();
            this.deviceAccessLayerMock.WriteToDeviceAsync(deviceId, address, sentData).ThrowsForAnyArgs(exception);

            //act
            await this.cut.SetDeviceStateAsync(deviceId, deviceState);
        }

        [Test]
        public async Task ShallWriteToDeviceLayerWhenSetDeviceState()
        {
            //assert
            var deviceId = this.fixture.Create<byte>();
            var deviceState = this.fixture.Create<DeviceState>();
            const int DataLength = 1;
            var sentData = this.fixture.CreateMany<ushort>(DataLength).ToArray();
            sentData[0] = (ushort)deviceState;

            //act
            await this.cut.SetDeviceStateAsync(deviceId, deviceState);

            //assert
            await
                this.deviceAccessLayerMock.Received(1)
                    .WriteToDeviceAsync(
                        deviceId,
                        Registry.STATE_ADDR,
                        Arg.Is<ushort[]>(x => x.Length == DataLength && x[0] == (ushort)deviceState));
        }
    }
}