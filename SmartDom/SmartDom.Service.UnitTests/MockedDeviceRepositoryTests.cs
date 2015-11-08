// SmartDom
// SmartDom.Service.UnitTests
// MockedDeviceRepositoryTests.cs
//  
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
//  

namespace SmartDom.Service.UnitTests
{
    using System;
    using System.Data;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using NSubstitute;
    using NSubstitute.ExceptionExtensions;

    using NUnit.Framework;

    using Ploeh.AutoFixture;

    using ServiceStack.Data;
    using ServiceStack.Logging;
    using ServiceStack.OrmLite;

    using SmartDom.Service.Database;
    using SmartDom.Service.Interface.Models;

    [TestFixture]
    public class MockedDeviceRepositoryTests
    {
        [SetUp]
        public void Init()
        {
            this.fx = new Fixture();
            this.logger = Substitute.For<ILog>();
            this.dbConnection = Substitute.For<IDbConnection>();
            this.ormWrapper = Substitute.For<IOrmWrapper>();
            this.dbFactory = Substitute.For<IDbConnectionFactory>();
        }

        private Fixture fx;

        private ILog logger;

        private IDbConnection dbConnection;

        private IOrmWrapper ormWrapper;

        private IDbConnectionFactory dbFactory;

        [Test]
        public async Task ShallInvokeLoggerWhenExceptionOnExistAsync()
        {
            //arrange

            var exception = this.fx.Create<Exception>();

            this.ormWrapper.ExistsAsync(this.dbConnection, Arg.Any<Expression<Func<Device, bool>>>())
                .ThrowsForAnyArgs(exception);

            this.dbFactory.Open().Returns(x => this.dbConnection);
            var deviceId = this.fx.Create<byte>();

            this.fx.Inject(this.dbFactory);
            this.fx.Inject(this.logger);
            this.fx.Inject(this.ormWrapper);

            var cut = this.fx.Create<DeviceDbRepository>();

            //act
            var exist = await cut.ExistAsync(x => x.Id == deviceId);

            //assert
            Assert.IsFalse(exist);
            this.logger.Received().Error(Arg.Is(exception));
        }

        [Test]
        public async Task ShallInvokeLoggerWhenExceptionOnGetAllAsync()
        {
            //arrange
            var exception = this.fx.Create<Exception>();
            this.ormWrapper.SelectAsync<Device>(this.dbConnection).ThrowsForAnyArgs(exception);

            this.dbFactory.Open().Returns(x => this.dbConnection);

            this.fx.Inject(this.dbFactory);
            this.fx.Inject(this.logger);
            this.fx.Inject(this.ormWrapper);

            var cut = this.fx.Create<DeviceDbRepository>();

            //act
            var receivedDevices = await cut.GetAllAsync();

            //assert
            Assert.IsNotNull(receivedDevices);
            Assert.AreEqual(0, receivedDevices.Count);
            this.logger.Received().Error(Arg.Is(exception));
        }

        [Test]
        public async Task ShallInvokeLoggerWhenExceptionOnInsertAsync()
        {
            //arrange
            var exception = this.fx.Create<Exception>();
            var device = this.fx.Create<Device>();
            this.ormWrapper.InsertAsync(this.dbConnection, Arg.Any<Device>())
                .ThrowsForAnyArgs(exception);

            this.dbFactory.Open().Returns(x => this.dbConnection);

            this.fx.Inject(this.dbFactory);
            this.fx.Inject(this.logger);
            this.fx.Inject(this.ormWrapper);

            var cut = this.fx.Create<DeviceDbRepository>();

            //act
            await cut.InsertAsync(device);

            //assert
            this.logger.Received().Error(Arg.Is(exception));
        }
    }
}