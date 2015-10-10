//
// SmartDom
// SmartDom.Service
// IDeviceManager.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
// 

using SmartDom.Service.Interface.Models;
using System.Threading.Tasks;

namespace SmartDom.Service
{
    public interface IDeviceManager
    {
        Task<Device> GetDevice(byte deviceId);
        Task SetDeviceState(byte deviceId, DeviceState deviceState);
    }
}
