// SmartDom
// SmartDom.Client.UnitTests
// ClientTests.cs
//  
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
//  

namespace SmartDom.Client.UnitTests
{
    using System.Threading.Tasks;

    using NSubstitute;

    using NUnit.Framework;

    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoNSubstitute;

    using ServiceStack;

    using SmartDom.Service.Interface.Messages;
    using SmartDom.Service.Interface.Models;

    [TestFixture]
    public class ClientTests
    {
        [SetUp]
        public void Init()
        {
            this.fixture =
                new Fixture().Customize(new AutoConfiguredNSubstituteCustomization())
                    .Customize(new RandomRangedNumberCustomization());

            this.restClientMock = this.fixture.Freeze<IRestClientAsync>();
            this.cut = new Client(this.restClientMock);
        }

        /// <summary>
        ///     Class under tests
        /// </summary>
        private IClient cut;

        private IFixture fixture;

        private IRestClientAsync restClientMock;

        [Test]
        public async Task ShallInvokeGetAsyncWhenGetDeviceStateWasCalled()
        {
            //arrange
            var deviceId = this.fixture.Create<byte>();
            var predicate = Arg.Is<GetDeviceRequest>(x => x.Id.Equals(deviceId));
            var device = this.fixture.Build<Device>().With(x => x.Id, deviceId).Create();
            var response = this.fixture.Build<GetDeviceResponse>().With(x => x.Result, device).Create();
            this.restClientMock.GetAsync(predicate).Returns(response);

            //act
            var receivedState = await this.cut.GetDeviceStateAsync(deviceId);

            //assert
            Assert.AreEqual(device.State, receivedState);
        }

        [Test]
        public async Task ShallInvokeGetAsyncWhenGetDevicesWasCalled()
        {
            await this.cut.GetDevicesAsync();
            await this.restClientMock.Received(1).GetAsync(Arg.Any<GetDevicesRequest>());
        }

        [Test]
        public async Task ShallInvokeGetWithGivenId()
        {
            //arrange
            var deviceId = this.fixture.Create<byte>();

            //act
            await this.cut.GetDeviceAsync(deviceId);

            //assert
            await this.restClientMock.Received(1).GetAsync(Arg.Is<GetDeviceRequest>(x => x.Id.Equals(deviceId)));
        }

        [Test]
        public void ShallInvokePostWithGivenDevice()
        {
            //arrange
            var deviceId = this.fixture.Create<byte>();
            var deviceType = this.fixture.Create<DeviceType>();
            var deviceSubtype = this.fixture.Create<DeviceSubtype>();

            //act
            this.cut.AddDeviceAsync(deviceId, deviceType, deviceSubtype);

            //assert
            this.restClientMock.Received(1)
                .PostAsync(Arg.Is<AddDeviceRequest>(x => x.DeviceId == deviceId 
                && x.DeviceType == deviceType && x.DeviceSubType == deviceSubtype));
        }

        [Test]
        public async Task ShallInvokePutWithGivenDeviceState()
        {
            //arrange
            var deviceId = this.fixture.Create<byte>();
            var deviceState = this.fixture.Create<DeviceState>();

            //act
            await this.cut.SetDeviceStateAsync(deviceId, deviceState);

            //assert
            await
                this.restClientMock.Received(1)
                    .PutAsync(Arg.Is<SetDeviceStateRequest>(x => x.Id.Equals(deviceId) && x.State.Equals(deviceState)));
        }

        [Test]
        public async Task ShallInvokeRemoveWithGivenDeviceState()
        {
            //arrange
            var deviceId = this.fixture.Create<byte>();
          
            //act
            await this.cut.RemoveDeviceAsync(deviceId);

            //assert
            await
                this.restClientMock.Received(1)
                    .DeleteAsync(Arg.Is<RemoveDeviceRequest>(x => x.Id.Equals(deviceId)));
        }
    }
}