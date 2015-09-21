//
// SmartDom
// SmartDom.Service
// DeviceManagementService.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
// 

namespace SmartDom.Service
{
    using System.Collections.Generic;
    using SmartDom.Service.Interface;
    using SmartDom.Service.Interface.DataModel;
    using SmartDom.Service.Interface.Messages;
    using ServiceStackBase = ServiceStack.ServiceInterface.Service;

    public class DeviceManagementService : ServiceStackBase, IDeviceManagementService
    {
        public IDeviceManager DeviceManager { get; set; }

        public DeviceManagementService(IDeviceManager deviceManager)
        {
            this.DeviceManager = deviceManager;
        }

        public IResponse<Device> Get(GetDeviceRequest request)
        {
            return new GetDeviceResponse
            {
                Result = DeviceManager.GetDevice(request.Id)
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
            DeviceManager.SetDeviceState(request.Id, request.State);
        }
    }
}
