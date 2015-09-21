//
// SmartDom
// SmartDom.Service.Interface
// IDeviceAccessLayer.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
// 

namespace SmartDom.Service.Interface
{
    public interface IDeviceAccessLayer
    {
        ushort[] ReadFromDevice(byte deviceId, ushort startReadAddress,
            ushort numberOfPointsToRead);

        void WriteToDevice(byte deviceId, ushort startWriteAddress, ushort[] data);
    }
}
