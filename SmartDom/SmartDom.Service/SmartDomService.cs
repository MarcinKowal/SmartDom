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
            var deviceExist = await this.deviceRepository
                .ExistAsync(x => x.Id == request.Id);
            if (!deviceExist)
            {
                throw new HttpError(HttpStatusCode.NotFound, "");
            }
          
            return
                ResponseFactory.CreateResponse<GetDeviceResponse, Device>(
                    await this.deviceManager.GetDeviceAsync(request.Id));
        }

        public async Task<IResponse<IList<Device>>> Get(GetDevicesRequest request)
        {
            var devices = await this.deviceRepository.GetAllAsync();

            return
                ResponseFactory.CreateResponse<GetDevicesResponse, IList<Device>>(
                    await this.EnumerateDeviceStateById(devices.Select(x => x.Id)));
        }

        public async Task<IResponse<Device>> Post(AddDeviceRequest request)
        {
            var alreadyExist = await this.deviceRepository
                .ExistAsync(x => x.Id == request.DeviceId);
            if (alreadyExist)
            {
                throw HttpError.Conflict(string.Format("Device with ID {0} already exists",request.DeviceId));
            }
            var device = new Device
                             {
                                 Id = request.DeviceId,
                                 Type = request.DeviceType,
                                 Subtype = request.DeviceSubType
                             };
            await this.deviceRepository.InsertAsync(device);
            return ResponseFactory.CreateResponse<AddDeviceResponse,Device>(device);
        }

        public async Task Delete(RemoveDeviceRequest request)
        {
            await this.deviceRepository.DeleteAsync(x => x.Id == request.Id);
        }

        public async Task Put(SetDeviceStateRequest request)
        {
            var deviceExist = await this.deviceRepository
                .ExistAsync(x => x.Id == request.Id);
            if (!deviceExist)
            {
                throw new HttpError(HttpStatusCode.NotFound, "");
            }
            await this.deviceManager.SetDeviceStateAsync(request.Id, request.State);
        }

        /// <summary>
        /// Enumerates the device state by identifier.
        /// </summary>
        /// <param name="deviceIds">The device ids.</param>
        private async Task<IList<Device>> EnumerateDeviceStateById(IEnumerable<byte> deviceIds)
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