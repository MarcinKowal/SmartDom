//
// SmartDom
// SmartDom.Service.Interface
// ISmartDomRestService.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
// 

namespace SmartDom.Service.Interface
{
    using Messages;
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISmartDomRestService
    {
        Task<IResponse<Device>> Get(GetDeviceRequest request);
        Task<IResponse<IList<Device>>> Get(GetDevicesRequest request);
        Task Post(AddDeviceRequest request);
        Task Delete(RemoveDeviceRequest request);
        Task Put(SetDeviceStateRequest request);
    }
}
