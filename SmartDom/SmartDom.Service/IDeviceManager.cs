//
// SmartDom
// SmartDom.Service
// IDeviceManager.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
// 


namespace SmartDom.Service
{
    using Interface.Models;
    using System.Threading.Tasks;

    public interface IDeviceManager
    {
        Task<Device> GetDeviceAsync(byte deviceId);
        Task SetDeviceStateAsync(byte deviceId, DeviceState deviceState);
    }
}
