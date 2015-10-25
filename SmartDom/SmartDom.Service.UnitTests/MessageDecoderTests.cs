// SmartDom
// SmartDom.Service.UnitTests
// MessageDecoderTests.cs
//  
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
//  

namespace SmartDom.Service.UnitTests
{
    using System;
    using System.Linq;

    using NUnit.Framework;

    using Ploeh.AutoFixture;

    using SmartDom.Service.Interface.Models;

    [TestFixture]
    public class MessageDecoderTests
    {
        [SetUp]
        public void Init()
        {
            this.fixture = new Fixture();
            this.cut = this.fixture.Create<MessageDecoder>();
        }

        private IFixture fixture;

        private IMessageDecoder cut;

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        [Description("0 is reserved device id, modbus use is for broadcasting ")]
        public void ShallThrowExceptionWhenDecodedDeviceIdIsZero()
        {
            int length = Registry.TOTAL_REGS_SIZE;
            var msg = this.fixture.CreateMany<ushort>(length).ToArray();
            msg[Registry.ID_ADDR] = 0;
            msg[Registry.TYPE_ADDR] = (ushort)this.fixture.Create<DeviceType>();
            msg[Registry.SUBTYPE_ADDR] = (ushort)this.fixture.Create<DeviceSubtype>();
            msg[Registry.STATE_ADDR] = (ushort)this.fixture.Create<DeviceState>();

            this.cut.Decode(msg);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShallThrowExceptionWhenMessageToDecodeIsNull()
        {
            this.cut.Decode(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ShallThrowExceptionWhenMessageToDecodeIsTooShort()
        {
            var length = new Random().Next(0, Registry.TOTAL_REGS_SIZE);
            var data = this.fixture.CreateMany<ushort>(length).ToArray();
            this.cut.Decode(data);
        }
    }
}