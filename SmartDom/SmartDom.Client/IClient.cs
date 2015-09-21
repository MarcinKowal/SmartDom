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

    public interface IClient : IAsyncClient
    {
        void AddDevice(Device device);
        Device GetDevice(byte deviceId);
        IList<Device> GetDevices();
        void RemoveDevice(byte deviceId);
        void SetDeviceState(byte deviceId, DeviceState deviceState);
        DeviceState GetDeviceState(byte deviceId);
    }
}
