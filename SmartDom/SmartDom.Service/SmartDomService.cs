//
// SmartDom
// SmartDom.Service
// SmartDomService.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
// 

using SmartDom.Service.Interface.Models;

namespace SmartDom.Service
{
    using System.Collections.Generic;
    using Interface;
    using Interface.Messages;
    using ServiceStackBase = ServiceStack.ServiceInterface.Service;

#if !DEBUG
    [Authenticate]
#endif

    public class SmartDomService : ServiceStackBase, ISmartDomRestService
    {
        private readonly IDeviceManager deviceManager;

        public SmartDomService(IDeviceManager deviceManager)
        {
            this.deviceManager = deviceManager;
        }

        public IResponse<Device> Get(GetDeviceRequest request)
        {
            return new GetDeviceResponse
            {
                Result = deviceManager.GetDevice(request.Id)
            };
        }

        public IResponse<IList<Device>> Get(GetDevicesRequest request)
        {
            //var devices =  Repository.GetAll().ToList();
            return new GetDevicesResponse { Result = new List<Device> { new Device { Id = 44}} };
        }

        public void Post(AddDeviceRequest request)
        {
            //Repository.AddDevice(request.DeviceItem);
        }

        public void Delete(RemoveDeviceRequest request)
        {
            //Repository.DeleteDevice(request.Id);
        }

        public void Put(SetDeviceStateRequest request)
        {
            deviceManager.SetDeviceState(request.Id, request.State);
        }
    }
}
