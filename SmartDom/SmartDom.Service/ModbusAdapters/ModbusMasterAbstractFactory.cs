//
// SmartDom
// SmartDom.Service
// ModbusMasterAbstractFactory.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
// 

namespace SmartDom.Service.ModbusAdapters
{
    using MediaAdapters;

    public abstract class ModbusMasterAbstractFactory<T>
    {
        /// <summary>
        /// Creates the specified instance of IModbusMasterAdapter
        /// </summary>
        /// <param name="media">The media.</param>
        /// <returns></returns>
        public abstract IModbusMasterAdapter Create(MediaAbstractAdapter<T> media);
    }
}
