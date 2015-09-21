//
// SmartDom
// SmartDom.Service.Interface
// IDeviceManagementService.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
// 

namespace SmartDom.Service.Interface
{
    using System.Collections.Generic;
    using DataModel;
    using Messages;

    public interface IDeviceManagementService
    {
        IResponse<Device> Get(GetDeviceRequest request);
        IResponse<IList<Device>> Get(GetDevicesRequest request);
        void Post(AddDeviceRequest request);
        void Delete(RemoveDeviceRequest request);
        void Put(SetDeviceStateRequest request);
    }
}
