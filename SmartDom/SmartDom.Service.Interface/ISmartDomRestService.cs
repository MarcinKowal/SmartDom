//
// SmartDom
// SmartDom.Service.Interface
// ISmartDomRestService.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
// 

using SmartDom.Service.Interface.Models;

namespace SmartDom.Service.Interface
{
    using System.Collections.Generic;
    using Messages;

    public interface ISmartDomRestService
    {
        IResponse<Device> Get(GetDeviceRequest request);
        IResponse<IList<Device>> Get(GetDevicesRequest request);
        void Post(AddDeviceRequest request);
        void Delete(RemoveDeviceRequest request);
        void Put(SetDeviceStateRequest request);
    }
}
