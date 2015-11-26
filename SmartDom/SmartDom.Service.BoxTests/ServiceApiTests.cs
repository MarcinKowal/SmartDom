// SmartDom
// SmartDom.Service.BoxTests
// ServiceApiTests.cs
//  
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
//  

namespace SmartDom.Service.BoxTests
{
    using System.Threading.Tasks;

    using NUnit.Framework;

    using SmartDom.Client;
    using SmartDom.Host;
    using SmartDom.Service.Interface.Models;

    [TestFixture]
    public class ServiceApiTests
    {
        private const byte DeviceId = 1;
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
            var expectedState = DeviceState.Enabled;

            var device = await this.AddDevice();

            await this.client.SetDeviceStateAsync(device.Id, expectedState);
            var currentState = await this.client.GetDeviceStateAsync(device.Id);

            Assert.AreEqual(expectedState, currentState);

            expectedState = DeviceState.Disabled;
            await this.client.SetDeviceStateAsync(device.Id, expectedState);
            currentState = await this.client.GetDeviceStateAsync(device.Id);

            Assert.AreEqual(expectedState, currentState);

            await this.client.RemoveDeviceAsync(device.Id);
            var receivedDevices = await this.client.GetDevicesAsync();
            Assert.AreEqual(0,receivedDevices.Count);
        }

        [Test]
        public async void ShallReturnAllDevices()
        {
            var devices = await this.client.GetDevicesAsync();
            Assert.IsNotNull(devices);
            Assert.AreEqual(0,devices.Count);

            var device = await this.AddDevice();
            devices = await this.client.GetDevicesAsync();
            Assert.IsNotNull(devices);
            Assert.AreEqual(1,devices.Count);
        }

        [Test]
        public async void ShallReturnDeviceWithGivenId()
        {
            var addedDevice = await this.AddDevice();
            var device = await this.client.GetDeviceAsync(addedDevice.Id);
            Assert.IsNotNull(device);
            Assert.AreEqual(DeviceId, device.Id);
        }

        private async Task<Device> AddDevice()
        {
            return await this.client.AddDeviceAsync(DeviceId, DeviceType.Switch, DeviceSubtype.Digital);
        }

    }
}