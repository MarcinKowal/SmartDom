// SmartDom
// SmartDom.Service
// SmartDomService.cs
//  
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
//  

namespace SmartDom.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    using ServiceStack;

    using SmartDom.Service.Database;
    using SmartDom.Service.Interface;
    using SmartDom.Service.Interface.Messages;
    using SmartDom.Service.Interface.Models;

#if !DEBUG
    [Authenticate]
#endif

    public class SmartDomService : Service, ISmartDomRestService
    {
        private readonly IDeviceManager deviceManager;
        private readonly IGenericRepository<Device> deviceRepository;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SmartDomService" /> class.
        /// </summary>
        /// <param name="deviceManager">The device manager.</param>
        /// <param name="deviceRepository">The device repository.</param>
        public SmartDomService(IDeviceManager deviceManager, IGenericRepository<Device> deviceRepository)
        {
            this.deviceManager = deviceManager;
            this.deviceRepository = deviceRepository;
        }

        public async Task<IResponse<Device>> Get(GetDeviceRequest request)
        {
            var deviceExist = await this.deviceRepository.ExistAsync(x => x.Id == request.Id);
            if (!deviceExist)
            {
                throw new HttpError(HttpStatusCode.NotFound, "");
            }
            var device = await this.deviceManager.GetDeviceAsync(request.Id);
            return new GetDeviceResponse { Result = device };
        }

        public async Task<IResponse<IList<Device>>> Get(GetDevicesRequest request)
        {
            // temporary, it will be taken from db
            var de = await this.deviceRepository.GetAllAsync();
            var devices = new List<Device> { new Device { Id = 1 } };
            return new GetDevicesResponse { Result = await this.EnumerateDevicesById(devices.Select(x => x.Id)) };
        }

        public async Task Post(AddDeviceRequest request)
        {
            var alreadyExist = await this.deviceRepository
                .ExistAsync(x => x.Id == request.Device.Id);
            if (alreadyExist)
            {
                throw new HttpError(HttpStatusCode.Conflict,"");
            }
            await this.deviceRepository.InsertAsync(request.Device);
        }

        public async Task Delete(RemoveDeviceRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task Put(SetDeviceStateRequest request)
        {
            var deviceExist = await this.deviceRepository.ExistAsync(x => x.Id == request.Id);
            if (!deviceExist)
            {
                throw new HttpError(HttpStatusCode.NotFound, "");
            }
            await this.deviceManager.SetDeviceStateAsync(request.Id, request.State);
        }

        private async Task<IList<Device>> EnumerateDevicesById(IEnumerable<byte> deviceIds)
        {
            var devices = new List<Device>();
            foreach (var deviceId in deviceIds)
            {
                var device = await this.deviceManager.GetDeviceAsync(deviceId);
                devices.Add(device);
            }
            return devices;
        }
    }
}