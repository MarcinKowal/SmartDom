// SmartDom
// SmartDom.Service
// SerialPortAdapter.cs
//  
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
//  

namespace SmartDom.Service.MediaAdapters
{
    using System;
    using System.IO.Ports;

    using SmartDom.Service.Interface;

    public class SerialPortAdapter : MediaAbstractAdapter<SerialPort>
    {
        private readonly IConfigurationRepository configuration;

        private readonly SerialPort serialPort;

        public SerialPortAdapter(IConfigurationRepository configuration)
        {
            this.configuration = configuration;

            this.serialPort = new SerialPort
                                  {
                                      BaudRate = configuration.GetConfigurationValue<int>("server", "Baud"),
                                      DataBits = configuration.GetConfigurationValue<int>("server", "DataBit"),
                                      Parity =
                                          configuration.GetConfigurationValue<Parity>("server", "ParityBit"),
                                      StopBits =
                                          configuration.GetConfigurationValue<StopBits>("server", "StopBit"),
                                      PortName =
                                          configuration.GetConfigurationValue<string>("server", "SerialPort"),
                                      ReadTimeout =
                                          configuration.GetConfigurationValue<int>("server", "ReadTimeout"),
                                      WriteTimeout =
                                          configuration.GetConfigurationValue<int>("server", "WriteTimeout"),
                                  };
        }

        public override SerialPort Media
        {
            get
            {
                return this.serialPort;
            }
        }

        public override void Initialize()
        {
            if (!this.serialPort.IsOpen)
            {
                this.serialPort.Open();
            }
        }

        public override void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.serialPort != null)
                {
                    this.serialPort.Dispose();
                }
            }
        }
    }
}