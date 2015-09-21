#region Copyrights
//
// SmartDom
// SmartDom.Service
// RtuSerialModbusMasterFactory.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
//  
#endregion

namespace SmartDom.Service.ModbusAdapters
{
    using System.IO.Ports;
    using MediaAdapters;

    public class RtuSerialModbusMasterFactory : ModbusMasterAbstractFactory<SerialPort>
    {
        /// <summary>
        /// Creates the specified instance
        /// </summary>
        /// <param name="media">The media.</param>
        /// <returns></returns>
        public override IModbusMasterAdapter Create(MediaAbstractAdapter<SerialPort> media)
        {
            media.Initialize();
            return new RtuSerialModbusMaster(media);
        }
    }
}