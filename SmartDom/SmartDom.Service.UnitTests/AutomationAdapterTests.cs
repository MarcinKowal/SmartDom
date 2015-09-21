//using System;
//using Moq;
//using NUnit.Framework;
//using SmartDom.Configuration;
//using SmartDom.Modbus;

//namespace SmartDom.Service.UnitTests
//{
//    [TestFixture]
//    public class AutomationAdapterTests
//    {
//        //private AutomationAdapter automationAdapter;
//        private Mock<IModbusMasterFactory> modbusMasterFactoryMock;
//        private Mock<IServerConfiguration> configurationMock;
//        private Mock<IMessageDecoder> decoderMock;
//        private Mock<IMessageEncoder> encoderMock;

//        [SetUp]
//        public void Init()
//        {
//            modbusMasterFactoryMock = new Mock<IModbusMasterFactory>();
//            configurationMock = new Mock<IServerConfiguration>();
//            decoderMock = new Mock<IMessageDecoder>();
//            encoderMock = new Mock<IMessageEncoder>();

//            automationAdapter = 
//                new AutomationAdapter(modbusMasterFactoryMock.Object, 
//                    configurationMock.Object, encoderMock.Object, decoderMock.Object);
//        }

//        [Test]
//        [ExpectedException(typeof(ArgumentNullException))]
//        public void ConstructorShallThrowExceptionWhenModbusFactoryIsNull()
//        {
//            new AutomationAdapter(null, It.IsAny<IServerConfiguration>(),
//                It.IsAny<IMessageEncoder>(), It.IsAny<IMessageDecoder>());
//        }

//        [Test]
//        [ExpectedException(typeof(ArgumentNullException))]
//        public void ConstructorShallThrowExceptionWhenModbusConfigurationIsNull()
//        {
//            new AutomationAdapter(modbusMasterFactoryMock.Object, null,
//                It.IsAny<IMessageEncoder>(), It.IsAny<IMessageDecoder>());
//        }

//        [Test]
//        [ExpectedException(typeof(ArgumentNullException))]
//        public void ConstructorShallThrowExceptionWhenEncoderIsNull()
//        {
//            new AutomationAdapter(modbusMasterFactoryMock.Object,configurationMock.Object,
//               null, It.IsAny<IMessageDecoder>());
//        }

//        [Test]
//        [ExpectedException(typeof(ArgumentNullException))]
//        public void ConstructorShallThrowExceptionWhenDecoderIsNull()
//        {
//            new AutomationAdapter(modbusMasterFactoryMock.Object, configurationMock.Object,
//               encoderMock.Object, null);
//        }


//    }
//}
