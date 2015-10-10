using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;
using ServiceStack;
using SmartDom.Service.Interface.Messages;
using SmartDom.Service.Interface.Models;
using System.Threading.Tasks;

namespace SmartDom.Client.UnitTests
{
    [TestFixture]
    public class ClientTests
    {
        /// <summary>
        /// Class under tests
        /// </summary>
        private IClient cut;
        private IFixture fixture;
        private IRestClientAsync restClientMock;

        [SetUp]
        public void Init()
        {
            fixture = new Fixture()
                .Customize(new AutoConfiguredNSubstituteCustomization())
                .Customize(new RandomRangedNumberCustomization());

            restClientMock = fixture.Freeze<IRestClientAsync>();
            cut = new Client(restClientMock);
        }

        [Test]
        [Ignore("todo method not implemented yet")]
        public void ShallInvokePostWithGivenDevice()
        { 
            var device = fixture.Create<Device>();
            cut.AddDeviceAsync(device);
            restClientMock.Received(1)
                .PostAsync(Arg.Is<AddDeviceRequest>(x => x.DeviceItem.Equals(device)));
        }

        [Test]
        public async Task ShallInvokePutWithGivenDeviceState()
        {   
            //arrange
            var deviceId = fixture.Create<byte>();
            var deviceState = fixture.Create<DeviceState>();

            //act
            await cut.SetDeviceStateAsync(deviceId, deviceState);
           
            //assert
            await restClientMock.Received(1)
                .PutAsync(Arg.Is<SetDeviceStateRequest>(x => x.Id.Equals(deviceId)
                    && x.State.Equals(deviceState))); 
        }

        [Test]
        public async Task ShallInvokeGetWithGivenId()
        {
            //arrange
            var deviceId = fixture.Create<byte>();

            //act
            await cut.GetDeviceAsync(deviceId);

            //assert
            await restClientMock.Received(1)
                            .GetAsync(Arg.Is<GetDeviceRequest>(x => x.Id.Equals(deviceId)));
        }

        [Test]
        public async Task ShallInvokeGetAsyncWhenGetDevicesWasCalled()
        {
            await cut.GetDevicesAsync();
            await restClientMock.Received(1)
                            .GetAsync(Arg.Any<GetDevicesRequest>());
        }

        [Test]
        public async Task ShallInvokeGetAsyncWhenGetDeviceStateWasCalled()
        { 
            //arrange
            var deviceId = fixture.Create<byte>();
            var predicate = Arg.Is<GetDeviceRequest>(x => x.Id.Equals(deviceId));
            var device = fixture.Build<Device>()
                .With(x => x.Id, deviceId).Create();
            var response = fixture.Build<GetDeviceResponse>()
                .With(x => x.Result,device).Create();
          restClientMock.GetAsync(predicate).Returns(response);

            //act
            var receivedState = await cut.GetDeviceStateAsync(deviceId);

            //assert
            Assert.AreEqual(device.State, receivedState);
        }
    }
}
