// SmartDom
// SmartDom.Service
// RtuSerialModbusMaster.cs
//  
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
//  

namespace SmartDom.Service.ModbusAdapters
{
    using System;
    using System.IO.Ports;
    using System.Threading.Tasks;

    using Modbus.Device;

    using SmartDom.Service.MediaAdapters;

    public class RtuSerialModbusMaster : IModbusMasterAdapter
    {
        private readonly ModbusMaster modbus;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RtuSerialModbusMaster" /> class.
        /// </summary>
        /// <param name="mediaAbstractAdapter">The media abstract adapter.</param>
        public RtuSerialModbusMaster(MediaAbstractAdapter<SerialPort> mediaAbstractAdapter)
        {
            this.modbus = ModbusSerialMaster.CreateRtu(mediaAbstractAdapter.Media);
        }

        /// <summary>
        ///     Reads the holding registers.
        /// </summary>
        /// <param name="slaveAddress">The slave address.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="numberOfPoints">The number of points.</param>
        /// <returns></returns>
        public ushort[] ReadHoldingRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            return this.modbus.ReadHoldingRegisters(slaveAddress, startAddress, numberOfPoints);
        }

        /// <summary>
        ///     Reads the holding registers asynchronously
        /// </summary>
        /// <param name="slaveAddress">The slave address.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="numberOfPoints">The number of points.</param>
        /// <returns></returns>
        public async Task<ushort[]> ReadHoldingRegistersAsync(
            byte slaveAddress,
            ushort startAddress,
            ushort numberOfPoints)
        {
            return await this.modbus.ReadHoldingRegistersAsync(slaveAddress, startAddress, numberOfPoints);
        }

        /// <summary>
        ///     Writes the multiple registers.
        /// </summary>
        /// <param name="slaveAddress">The slave address.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="data">The data.</param>
        public void WriteMultipleRegisters(byte slaveAddress, ushort startAddress, ushort[] data)
        {
            this.modbus.WriteMultipleRegisters(slaveAddress, startAddress, data);
        }

        /// <summary>
        ///     Writes the multiple registers asynchronously
        /// </summary>
        /// <param name="slaveAddress">The slave address.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public async Task WriteMultipleRegistersAsync(byte slaveAddress, ushort startAddress, ushort[] data)
        {
            await this.modbus.WriteMultipleRegistersAsync(slaveAddress, startAddress, data);
        }

        // Dispose() calls Dispose(true)
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.modbus != null)
                {
                    this.modbus.Dispose();
                }
            }
        }
    }
}