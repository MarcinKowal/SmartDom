using NUnit.Framework;
using SmartDom.Client;
using SmartDom.Service.Interface.Models;
using SmartDom.Host;

namespace SmartDom.Service.BoxTests
{
    [TestFixture]
    public class ServiceApiTests
    {
        private IClientFactory clientFactory;
        private IClient client;

        
        [SetUp]
        public void Init()
        {
            this.clientFactory = new ClientFactory();

            var clientCredentials = new ClientCredentials("test","test");
            client = clientFactory.Create(Platform.ServerUri, ClientFormat.Json, clientCredentials);
        }

        [Test]
        public async void ShallReturnAllDevices()
        {
            var devices = await client.GetDevicesAsync();
            Assert.IsNotNull(devices);
        }

        [Test]
        public async void ShallReturnDeviceWithGivenId()
        {
            const byte DeviceId = 1;
            var device = await client.GetDeviceAsync(DeviceId);
            Assert.IsNotNull(device);
            Assert.AreEqual(DeviceId, device.Id);
        }

        [Test]
        public async void ShallChangeStateOfGivenDevice()
        {
            const byte DeviceId = 1;
            var expectedState = DeviceState.Enabled;
            await client.SetDeviceStateAsync(DeviceId, expectedState);
            var currentState = await client.GetDeviceStateAsync(DeviceId);

            Assert.AreEqual(expectedState, currentState);

            expectedState = DeviceState.Disabled;
            await client.SetDeviceStateAsync(DeviceId, expectedState);
            currentState = currentState = await client.GetDeviceStateAsync(DeviceId);

            Assert.AreEqual(expectedState, currentState);
        }
    }
}
