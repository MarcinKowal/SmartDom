//
// SmartDom
// SmartDom.Service
// SerialPortAdapter.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
// 

namespace SmartDom.Service.MediaAdapters
{
    using Interface;
    using System.IO.Ports;
    using System;

    public class SerialPortAdapter : MediaAbstractAdapter<SerialPort>
    {
        private readonly SerialPort serialPort;
        private readonly IConfigurationRepository configuration;

        public SerialPortAdapter(IConfigurationRepository configuration)
        {
            this.configuration = configuration;
            
            serialPort = new SerialPort
            {
                BaudRate = configuration.GetConfigurationValue<int>("server", "Baud"),
                DataBits = configuration.GetConfigurationValue<int>("server", "DataBit"),
                Parity = configuration.GetConfigurationValue<Parity>("server", "ParityBit"),
                StopBits = configuration.GetConfigurationValue<StopBits>("server", "StopBit"),
                PortName = configuration.GetConfigurationValue<string>("server", "SerialPort"),
                ReadTimeout = configuration.GetConfigurationValue<int>("server", "ReadTimeout"),
                WriteTimeout = configuration.GetConfigurationValue<int>("server", "WriteTimeout"),
            };
        }
        
        public override SerialPort Media
        {
            get { return serialPort; }
        }

        public override void Initialize()
        {
            if (!serialPort.IsOpen)
                serialPort.Open();
        }

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (serialPort != null)
                    serialPort.Dispose();
            }
        }
    }
}
