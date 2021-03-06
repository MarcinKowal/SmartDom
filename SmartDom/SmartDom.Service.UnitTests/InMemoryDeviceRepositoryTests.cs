﻿// SmartDom
// SmartDom.Service.UnitTests
// InMemoryDeviceRepositoryTests.cs
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
    using System.Threading.Tasks;

    using NUnit.Framework;

    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoNSubstitute;

    using ServiceStack.Data;
    using ServiceStack.OrmLite;
    using ServiceStack.OrmLite.Sqlite;

    using SmartDom.Service.Database;
    using SmartDom.Service.Interface.Models;

    [TestFixture]
    public class InMemoryDeviceRepositoryTests
    {
        private IGenericRepository<Device> cut;
        private IFixture fixture;
        private IDbConnectionFactory dbFactory;
        private IOrmWrapper ormWrapper;

        [SetUp]
        public void Init()
        {
            this.fixture = new Fixture()
                .Customize(new AutoNSubstituteCustomization());

            this.dbFactory = new OrmLiteConnectionFactory(
                connectionString: ":memory:",
                dialectProvider: SqliteOrmLiteDialectProvider.Instance);
            this.ormWrapper = this.fixture.Create<OrmWrapper>();
            this.fixture.Inject(this.dbFactory);
            this.fixture.Inject(this.ormWrapper);
            this.cut = this.fixture.Create<DeviceDbRepository>();

            using (var db = this.dbFactory.Open())
            {
                db.CreateTable<Device>(true);
            }
        }


        [Test]
        public async Task ShallReturnFalseWhenDeviceDoesNotExistInDb()
        {
            //arrange
            var deviceId = this.fixture.Create<byte>();

            //act
            var exist = await this.cut.ExistAsync(x => x.Id == deviceId);

            //assert 
            Assert.IsFalse(exist);
        }

        [Test]
        public async Task ShallReturnTrueWhenDeviceExistInDb()
        {
            //arrange
            var device = this.fixture.Create<Device>();
            using (var db = this.dbFactory.Open())
            {
                db.Insert(device);
            }

            //act
            var exist = await this.cut.ExistAsync(x => x.Id == device.Id);

            //assert 
            Assert.IsTrue(exist);
        }

        [Test]
        public async Task ShallReturnEmptyListWhenGetAllOnEmptyDb()
        {
            var devices = await this.cut.GetAllAsync();
            Assert.IsNotNull(devices);
            Assert.AreEqual(0, devices.Count());
        }

        [Test]
        public async Task ShallReturnAllDevicesWithoutFilter()
        {
            //arrange
            var expectedDevices = this.fixture.CreateMany<Device>(10).ToList();
            
            using (var db = this.dbFactory.Open())
            {
                foreach (var device in expectedDevices)
                {
                    db.Insert(device);
                }
            }
                
            //act
            var receivedDevices = await this.cut.GetAllAsync();

            //assert
            Assert.That(receivedDevices, Is.EquivalentTo(expectedDevices));
        }

        [Test]
        public async Task ShallReturnAllDevicesWithStateFilter()
        {
            //arrange
            var enabledDevice = this.fixture
                .Build<Device>().With(x => x.State, DeviceState.Enabled)
                .Create();

            var disabledDevice = this.fixture
                .Build<Device>().With(x => x.State, DeviceState.Disabled)
                .Create();

            var disabledDevice2 = this.fixture
                .Build<Device>().With(x => x.State, DeviceState.Disabled)
                .Create();

            var expectedDevices = new List<Device> { disabledDevice, enabledDevice, disabledDevice2 };

            using (var db = this.dbFactory.Open())
            {
                foreach (var device in expectedDevices)
                {
                    db.Insert(device);
                }
            }

            //act
            var receivedDevices = await this.cut.GetAllAsync(x => x.State == DeviceState.Disabled);

            //assert
            Assert.IsNotNull(receivedDevices);
            Assert.AreEqual(2, receivedDevices.Count);
            Assert.Contains(disabledDevice, receivedDevices);
            Assert.Contains(disabledDevice2, receivedDevices);
        }

        [Test]
        public async Task ShallInsertDevice()
        {
            //arrange
            var device = this.fixture.Create<Device>();

            //act 
            await this.cut.InsertAsync(device);

            //assert
            using (var db = this.dbFactory.Open())
            {
                var selectedDevices = await db.SelectAsync<Device>(x => x.Id == device.Id);
                Assert.AreEqual(selectedDevices.Count, 1);
                Assert.AreEqual(device,selectedDevices[0]);
            }
        }

        [Test]
        public async Task ShallRemoveDevicesWithFilter()
        {
            //arrange 
            var devicesGenerator = new Fixture().Create<Generator<Device>>();
            fixture.Customize<Device>(c => c.Without(x => x.State));
            fixture.Customize<IEnumerable<Device>>(
                c =>
                c.FromFactory(
                    () => Enum.GetValues(typeof(DeviceState)).Cast<DeviceState>()
                              .Select(s => devicesGenerator.First(u => u.State == s))));

            var devices = fixture.Create<IEnumerable<Device>>(); // returns devices with all supported states
            Expression<Func<Device, bool>> disabledDevicesFilter
                = x => x.State == DeviceState.Disabled;

            using (var db = this.dbFactory.Open())
            {
                foreach (var device in devices)
                {
                    db.Insert(device);
                }

                var totalCount = await db.CountAsync<Device>();
                Assert.AreEqual(devices.Count(), totalCount);

                //act
                var removedCount = await this.cut.DeleteAsync(disabledDevicesFilter);

                //assert
                Assert.AreEqual(1, removedCount);
                var remainingDevices = db.Select<Device>();
                Assert.IsFalse(remainingDevices
                    .Any(x => x.State == DeviceState.Disabled));
            }
        }
    }
}