//
// SmartDom
// SmartDom.Service
// IDeviceAccessLayer.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
// 

namespace SmartDom.Service.DeviceLayers
{
    public interface IDeviceAccessLayer
    {
        /// <summary>
        /// Reads from device.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="startReadAddress">The start read address.</param>
        /// <param name="numberOfPointsToRead">The number of points to read.</param>
        /// <returns></returns>
        ushort[] ReadFromDevice(byte deviceId, ushort startReadAddress,
            ushort numberOfPointsToRead);

        /// <summary>
        /// Writes to device.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="startWriteAddress">The start write address.</param>
        /// <param name="data">The data.</param>
        void WriteToDevice(byte deviceId, ushort startWriteAddress, ushort[] data);
    }
}
