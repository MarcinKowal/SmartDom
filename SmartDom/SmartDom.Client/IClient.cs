//
// SmartDom
// SmartDom.Client
// IClient.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
// 

using SmartDom.Service.Interface.Models;

namespace SmartDom.Client
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IClient
    {
        Task<Device> GetDeviceAsync(byte deviceId);
        Task<IList<Device>> GetDevicesAsync();
        Task<DeviceState> GetDeviceStateAsync(byte deviceId);
        Task SetDeviceStateAsync(byte deviceId, DeviceState deviceState);

        Task AddDeviceAsync(Device device);
        Task RemoveDeviceAsync(byte deviceId);
    }
}
