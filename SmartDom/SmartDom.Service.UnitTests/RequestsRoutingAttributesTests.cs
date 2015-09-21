using NUnit.Framework;
using SmartDom.Service.Interface.Messages;



namespace SmartDom.Service.UnitTests
{
    [TestFixture]
    public class RequestsRoutingAttributesTests
    {
        [Test]
        public void AddDeviceRequestHasRoutingAttribute()
        {
            AttributesExtension.HasRoutingAttribute(typeof (AddDeviceRequest), "POST", "/device");
        }

        [Test]
        public void GetDeviceRequestHasRoutingAttribute()
        {
            AttributesExtension.HasRoutingAttribute(typeof (GetDeviceRequest), "GET", "/device/{Id}");
        }

        [Test]
        public void GetDevicesRequestHasRoutingAttribute()
        {
            AttributesExtension.HasRoutingAttribute(typeof (GetDevicesRequest), "GET", "/devices");
        }

        [Test]
        public void SetDeviceStateRequestHasRoutingAttribute()
        {
            AttributesExtension.HasRoutingAttribute(typeof (SetDeviceStateRequest), "PUT", "/device/{Id}/state");
        }
    }
}
