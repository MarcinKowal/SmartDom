﻿// SmartDom
// SmartDom.Service
// SerialAccessLayer.cs
//  
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
//  

namespace SmartDom.Service.DeviceLayers
{
    using System.IO.Ports;
    using System.Threading.Tasks;

    using SmartDom.Service.MediaAdapters;
    using SmartDom.Service.ModbusAdapters;

    public class SerialAccessLayer : IDeviceAccessLayer
    {
        private readonly MediaAdapterAbstractFactory<SerialPort> mediaAdapterFactory;

        private readonly ModbusMasterAbstractFactory<SerialPort> modbusMasterFactory;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SerialAccessLayer" /> class.
        /// </summary>
        /// <param name="mediaAdapterFactory">The media adapter factory.</param>
        /// <param name="modbusMasterFactory">The modbus master factory.</param>
        public SerialAccessLayer(
            MediaAdapterAbstractFactory<SerialPort> mediaAdapterFactory,
            ModbusMasterAbstractFactory<SerialPort> modbusMasterFactory)
        {
            this.mediaAdapterFactory = mediaAdapterFactory;
            this.modbusMasterFactory = modbusMasterFactory;
        }

        /// <summary>
        ///     Reads from device.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="startReadAddress">The start read address.</param>
        /// <param name="numberOfPointsToRead">The number of points to read.</param>
        /// <returns></returns>
        public async Task<ushort[]> ReadFromDeviceAsync(
            byte deviceId,
            ushort startReadAddress,
            ushort numberOfPointsToRead)
        {
            using (var mediaAdapter = this.mediaAdapterFactory.Create())
            {
                using (var device = this.modbusMasterFactory.Create(mediaAdapter))
                {
                    return await device.ReadHoldingRegistersAsync(deviceId, startReadAddress, numberOfPointsToRead);
                }
            }
        }

        /// <summary>
        ///     Writes to device.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="startWriteAddress">The start write address.</param>
        /// <param name="data">The data.</param>
        public async Task WriteToDeviceAsync(byte deviceId, ushort startWriteAddress, ushort[] data)
        {
            using (var mediaAdapter = this.mediaAdapterFactory.Create())
            {
                using (var device = this.modbusMasterFactory.Create(mediaAdapter))
                {
                    await device.WriteMultipleRegistersAsync(deviceId, startWriteAddress, data);
                }
            }
        }
    }
}