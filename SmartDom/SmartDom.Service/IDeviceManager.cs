//
// SmartDom
// SmartDom.Service
// IDeviceManager.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
// 

using SmartDom.Service.Interface.Models;

namespace SmartDom.Service
{
    public interface IDeviceManager
    {
        Device GetDevice(byte deviceId);
        void SetDeviceState(byte deviceId, DeviceState deviceState);
    }
}
