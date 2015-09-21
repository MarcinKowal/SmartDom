//
// SmartDom
// SmartDom.Service
// IModbusMasterAdapter.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
// 

using System;
using System.Threading.Tasks;

namespace SmartDom.Service.ModbusAdapters
{
    public interface IModbusMasterAdapter : IDisposable 
    {
        /// <summary>
        /// Reads the holding registers.
        /// </summary>
        /// <param name="slaveAddress">The slave address.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="numberOfPoints">The number of points.</param>
        /// <returns></returns>
       ushort[] ReadHoldingRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints);
       /// <summary>
       /// Reads the holding registers asynchronously
       /// </summary>
       /// <param name="slaveAddress">The slave address.</param>
       /// <param name="startAddress">The start address.</param>
       /// <param name="numberOfPoints">The number of points.</param>
       /// <returns></returns>
       Task<ushort[]> ReadHoldingRegistersAsync(byte slaveAddress, ushort startAddress, ushort numberOfPoints);
       /// <summary>
       /// Writes the multiple registers.
       /// </summary>
       /// <param name="slaveAddress">The slave address.</param>
       /// <param name="startAddress">The start address.</param>
       /// <param name="data">The data.</param>
       void WriteMultipleRegisters(byte slaveAddress, ushort startAddress, ushort[] data);
       /// <summary>
       /// Writes the multiple registers asynchronously
       /// </summary>
       /// <param name="slaveAddress">The slave address.</param>
       /// <param name="startAddress">The start address.</param>
       /// <param name="data">The data.</param>
       /// <returns></returns>
       Task WriteMultipleRegistersAsync(byte slaveAddress, ushort startAddress, ushort[] data);
    }
}
