//
// SmartDom
// SmartDom.Service
// SmartDomService.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
// 

namespace SmartDom.Service
{
    using Interface.Models;
    using System.Collections.Generic;
    using Interface;
    using Interface.Messages;
    using System.Threading.Tasks;
    using System.Linq;
    using System;

#if !DEBUG
    [Authenticate]
#endif

    public class SmartDomService : ServiceStack.Service, ISmartDomRestService
    {
        private readonly IDeviceManager deviceManager;

        public SmartDomService(IDeviceManager deviceManager)
        {
            this.deviceManager = deviceManager;
        }

        public async Task<IResponse<Device>> Get(GetDeviceRequest request)
        {
            return new GetDeviceResponse
            {
                Result = await deviceManager.GetDevice(request.Id)
            };
        }

        public async Task<IResponse<IList<Device>>> Get(GetDevicesRequest request)
        {
            // temporary, it will be taken from db
            var devices = new List<Device> { new Device { Id = 1 } };
            return new GetDevicesResponse
            {
                Result = await EnumerateDevicesById(devices.Select(x => x.Id))
            };
        }

        private async Task<IList<Device>> EnumerateDevicesById(IEnumerable<byte> deviceIds)
        {
            var devices = new List<Device>();
            foreach (var deviceId in deviceIds)
            {
                var device = await deviceManager.GetDevice(deviceId);
                devices.Add(device);
            }
            return devices;
        }

        public async Task Post(AddDeviceRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(RemoveDeviceRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task Put(SetDeviceStateRequest request)
        {
            await deviceManager.SetDeviceState(request.Id, request.State);
        }
    }
}
