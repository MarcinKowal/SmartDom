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
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Net;
    using System.Threading.Tasks;

    using NSubstitute;

    using NUnit.Framework;

    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoNSubstitute;

    using ServiceStack;

    using Database;
    using Interface.Messages;
    using Interface.Models;

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
            try
            {
                await this.cut.Get(request);
            }
            catch (HttpError e)
            {
                Assert.AreEqual(HttpStatusCode.NotFound,e.StatusCode);
                throw;
            }
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

        [Test]
        public async Task ShallInvokeAddDeviceOnRepository()
        {
            //arrange
            var request = this.fixture.Create<AddDeviceRequest>();
            this.repository.ExistAsync(x => false)
               .ReturnsForAnyArgs(Task.FromResult(false));

            //act
            await this.cut.Post(request);

            //assert
            await this.repository
                .Received(1)
                .InsertAsync(Arg.Is<Device>(x=> x.Id == request.DeviceId 
                && x.Type == request.DeviceType
                && x.Subtype == request.DeviceSubType));

        }

        [Test]
        [ExpectedException(typeof(HttpError))]
        public async Task ShallThrowHttpConflictWhenDeviceExistsOnAddDeviceRequest()
        {
            //arrange
            var request = this.fixture.Create<AddDeviceRequest>();
            this.repository.ExistAsync(x => false)
                .ReturnsForAnyArgs(Task.FromResult(true));
            try
            {
                //act
                await this.cut.Post(request);
            }
            catch (HttpError e)
            {
                Assert.AreEqual(HttpStatusCode.Conflict,e.StatusCode);
                throw;
            }
        }

        [Test]
        public async Task ShallInvokeRemoveOnDeviceRepositoryOnRemoveDeviceRequest()
        {
            //arrange
            var request = this.fixture.Create<RemoveDeviceRequest>();
         
            //act
            await this.cut.Delete(request);

            //assert
             await this.repository.Received(1)
                .DeleteAsync(Arg.Any<Expression<Func<Device,bool>>>());
        }


        [Test]
        public async Task ShallNotEnumerateDevicesWhenRepositoryIsEmptyOnGetDevicesRequest()
        {
            //arrange
            var request = this.fixture.Create<GetDevicesRequest>();
            this.repository.GetAllAsync()
                .Returns(Task.FromResult(new List<Device>()));

            //act
            var response = await this.cut.Get(request);

            //assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Result);
            Assert.AreEqual(0,response.Result.Count);
            await this.deviceManager.DidNotReceiveWithAnyArgs()
                .GetDeviceAsync(Arg.Any<byte>());
        }

        [Test]
        public async Task ShallEnumarateAllDevicesOnGetDevicesRequest()
        {
            //arrange
            var devices = this.fixture.CreateMany<Device>().ToList();
            var ids = devices.Select(x => x.Id);
            var request = this.fixture.Create<GetDevicesRequest>();
            this.repository.GetAllAsync()
                .Returns(Task.FromResult(devices));

            //act
            var response = await this.cut.Get(request);

            //assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Result);
            Assert.AreEqual(devices.Count, response.Result.Count);

            foreach (var deviceId in ids)
            {
                await this.deviceManager.Received(1)
                    .GetDeviceAsync(deviceId);
            }
        }
    }
}