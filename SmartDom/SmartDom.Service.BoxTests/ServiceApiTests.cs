// SmartDom
// SmartDom.Service.BoxTests
// ServiceApiTests.cs
//  
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
//  

namespace SmartDom.Service.BoxTests
{
    using NUnit.Framework;

    using SmartDom.Client;
    using SmartDom.Host;
    using SmartDom.Service.Interface.Models;

    [TestFixture]
    public class ServiceApiTests
    {
        [SetUp]
        public void Init()
        {
            this.clientFactory = new ClientFactory();

            var clientCredentials = new ClientCredentials("test", "test");
            this.client = this.clientFactory.Create(Platform.ServerUri, ClientFormat.Json, clientCredentials);
        }

        private IClientFactory clientFactory;

        private IClient client;

        [Test]
        public async void ShallChangeStateOfGivenDevice()
        {
            const byte DeviceId = 1;
            var expectedState = DeviceState.Enabled;
            var device = new Device { Id = DeviceId };
            await this.client.AddDeviceAsync(device);
            await this.client.SetDeviceStateAsync(DeviceId, expectedState);
            var currentState = await this.client.GetDeviceStateAsync(DeviceId);

            Assert.AreEqual(expectedState, currentState);

            expectedState = DeviceState.Disabled;
            await this.client.SetDeviceStateAsync(DeviceId, expectedState);
            currentState = await this.client.GetDeviceStateAsync(DeviceId);

            Assert.AreEqual(expectedState, currentState);
        }

        [Test]
        public async void ShallReturnAllDevices()
        {
            const byte DeviceId = 1;
            var devices = await this.client.GetDevicesAsync();
            Assert.IsNotNull(devices);
            Assert.AreEqual(0,devices.Count);

            var device = new Device { Id = DeviceId };
            await this.client.AddDeviceAsync(device);
            devices = await this.client.GetDevicesAsync();
            Assert.IsNotNull(devices);
            Assert.AreEqual(1,devices.Count);
        }

        [Test]
        public async void ShallReturnDeviceWithGivenId()
        {
            const byte DeviceId = 1;
            await this.client.AddDeviceAsync(new Device { Id = DeviceId });
            var device = await this.client.GetDeviceAsync(DeviceId);
            Assert.IsNotNull(device);
            Assert.AreEqual(DeviceId, device.Id);
        }



    }
}