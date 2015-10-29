// SmartDom
// SmartDom.Service.UnitTests
// SmartDomServiceTests.cs
//  
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
//  

namespace SmartDom.Service.UnitTests
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using NSubstitute;

    using NUnit.Framework;

    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoNSubstitute;

    using ServiceStack;

    using SmartDom.Service.Database;
    using SmartDom.Service.Interface.Messages;
    using SmartDom.Service.Interface.Models;

    [TestFixture]
    public class SmartDomServiceTests
    {
        [SetUp]
        public void Init()
        {
            this.fixture = new Fixture().Customize(new AutoConfiguredNSubstituteCustomization());

            this.repository = this.fixture.Freeze<IGenericRepository<Device>>();
            this.deviceManager = this.fixture.Freeze<IDeviceManager>();
            this.cut = this.fixture.Create<SmartDomService>();
        }

        private IGenericRepository<Device> repository;

        private IDeviceManager deviceManager;

        private IFixture fixture;

        private SmartDomService cut;

        [Test]
        public async Task ShallReturnDeviceWhenDeviceExistOnGetDevice()
        {
            //arrange
            var request = this.fixture.Create<GetDeviceRequest>();
            var device = this.fixture.Build<Device>().With(x => x.Id, request.Id).Create();
            this.repository.ExistAsync(x => true).ReturnsForAnyArgs(Task.FromResult(true));
            this.deviceManager.GetDeviceAsync(request.Id).Returns(Task.FromResult(device));

            //act
            var response = await this.cut.Get(request);

            //assert
            Assert.IsNotNull(response);
            Assert.AreEqual(device, response.Result);
            await this.deviceManager.Received(1).GetDeviceAsync(request.Id);
        }

        [Test]
        [ExpectedException(typeof(HttpError))]
        public async Task ShallThrowHttpNotFoundWhenDeviceDoesNotExistInDbOnGetDevice()
        {
            var request = this.fixture.Create<GetDeviceRequest>();

            this.repository.ExistAsync(x => false).ReturnsForAnyArgs(Task.FromResult(false));
            await this.cut.Get(request);
        }


        [Test]
        [ExpectedException(typeof(HttpError))]
        public async Task ShallThrowHttpNotFoundWhenDeviceDoesNotExistOnSetDeviceState()
        {
            //arrange
            var request = this.fixture.Create<SetDeviceStateRequest>();
            this.repository.ExistAsync(x => false).ReturnsForAnyArgs(Task.FromResult(false));

            //act
            await this.cut.Put(request);
        }

        [Test]
        public async Task ShallIInvokeSetDeviceStateOnManagerWhenDeviceExistOnSetDeviceState()
        {
            //arrange
            var request = this.fixture.Create<SetDeviceStateRequest>();
            Expression<Func<Device, bool>> expression = x => x.Id == request.Id;
            this.repository.
                ExistAsync(Arg.Is<Expression<Func<Device, bool>>>(x => x == expression))
                .Returns(Task.FromResult(true));

            //act
            await this.cut.Put(request);

            //assert
            await this.deviceManager.Received(1).
                SetDeviceStateAsync(request.Id, request.State);

        }
    }
}