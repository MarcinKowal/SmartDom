// SmartDom
// SmartDom.Service
// SerialPortAdapterFactory.cs
//  
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
//  

namespace SmartDom.Service.MediaAdapters
{
    using System.IO.Ports;

    using SmartDom.Service.Interface;

    public class SerialPortAdapterFactory : MediaAdapterAbstractFactory<SerialPort>
    {
        private readonly IConfigurationRepository configurationRepository;

        public SerialPortAdapterFactory(IConfigurationRepository configurationRepository)
        {
            this.configurationRepository = configurationRepository;
        }

        public override MediaAbstractAdapter<SerialPort> Create()
        {
            return new SerialPortAdapter(this.configurationRepository);
        }
    }
}