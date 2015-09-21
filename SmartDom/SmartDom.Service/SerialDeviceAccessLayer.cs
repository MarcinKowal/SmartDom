//
// SmartDom
// SmartDom.Service
// ModbusRtuAccessLayer.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
// 

using Modbus.Device;
namespace SmartDom.Service
{
    using SmartDom.Service.Interface;
    using System.IO.Ports;
    using SmartDom.Service.MediaAdapters;

    
    public class SerialDeviceAccessLayer: IDeviceAccessLayer
    {
        public MediaAdapterFactory<SerialPort> MediaAdapterFactory { get; set; }
        public ModbusMasterFactory<SerialPort> ModbusMasterFactory { get; set; }


        public SerialDeviceAccessLayer(MediaAdapterFactory<SerialPort> mediaAdapterFactory, ModbusMasterFactory<SerialPort> modbusMasterFactory)
        {
            this.MediaAdapterFactory = mediaAdapterFactory;
            this.ModbusMasterFactory = modbusMasterFactory;
        }
        /// <summary>
        ///Performs a combination of one read operation and one write operation in a single Modbus transaction.  
        /// The write operation is performed before the read.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="startReadAddress">The start read address.</param>
        /// <param name="numberOfPointsToRead">The number of points to read.</param>
        /// <param name="startWriteAddress">The start write address.</param>
        /// <param name="writeData">The write data.</param>
        public ushort[] ReadFromDevice(byte deviceId, ushort startReadAddress,
            ushort numberOfPointsToRead)
        {
            using (var mediaAdapter = MediaAdapterFactory.Create())
            {
                //mediaAdapter.Initialize();
                using (var device = ModbusMasterFactory.Create(mediaAdapter))
                {
                    return device.ReadHoldingRegisters(deviceId, startReadAddress, numberOfPointsToRead);
                }
            }
        }

        public void WriteToDevice(byte deviceId, ushort startWriteAddress, ushort[] data)
        {
            using (var mediaAdapter = MediaAdapterFactory.Create())
            {
                //mediaAdapter.Initialize();
                using (var device = ModbusMasterFactory.Create(mediaAdapter))
                {
                    device.WriteMultipleRegisters(deviceId, startWriteAddress, data);
                }
            }
        }
    }
}
