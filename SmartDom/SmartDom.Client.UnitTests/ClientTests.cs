using NUnit.Framework;
using NSubstitute;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;
using SmartDom.Service.Interface.Messages;
using SmartDom.Service.Interface.Models;
using ServiceStack;

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
        private IRestClient restClientMock;

        [SetUp]
        public void Init()
        {
            fixture = new Fixture()
                .Customize(new AutoConfiguredNSubstituteCustomization())
                .Customize(new RandomRangedNumberCustomization());
            
            restClientMock = fixture.Freeze<IRestClient>();
            cut = new Client(restClientMock);
                
                
              //  fixture.Create<Client>();
        }

        [Test]
        public void ShallInvokePostWithGivenDevice()
        { 
            var device = fixture.Create<Device>();
            cut.AddDevice(device);
            restClientMock.Received(1)
                .Post(Arg.Is<AddDeviceRequest>(x => x.DeviceItem.Equals(device)));
        }

        [Test]
        public void ShallInvokePutWithGivenDeviceState()
        {   
            //arrange
            var deviceId = fixture.Create<byte>();
            var deviceState = fixture.Create<DeviceState>();

            //act
            cut.SetDeviceState(deviceId, deviceState);
        
            //assert
            restClientMock.Received()
                .Put(Arg.Is<SetDeviceStateRequest>(x=> x.Id.Equals(deviceId) 
                    && x.State.Equals(deviceState))); 
        }

        [Test]
        public void ShallInvokeGetWithGivenId()
        {
            //arrange
            var deviceId = fixture.Create<byte>();

            //act
            cut.GetDevice(deviceId);

            //assert
            restClientMock.Received()
                .Get(Arg.Is<GetDeviceRequest>(x => x.Id.Equals(deviceId)));
        }

        [Test]
        public void ShallInvokeGet()
        {
            cut.GetDevices();
            restClientMock.Received()
                .Get(Arg.Any<GetDevicesRequest>());
        }

        [Test]
        public void ShallInvokeGet2()
        { 
            //arrange
            var deviceId = fixture.Create<byte>();
            var predicate = Arg.Is<GetDeviceRequest>(x => x.Id.Equals(deviceId));
            var device = fixture.Build<Device>()
                .With(x => x.Id, deviceId).Create();
            var response = fixture.Build<GetDeviceResponse>()
                .With(x => x.Result,device).Create();
          restClientMock.Get(predicate).Returns(response);

            //act
            var receivedState = cut.GetDeviceState(deviceId);

            //assert
            Assert.AreEqual(device.State, receivedState);
        }
    }
}
