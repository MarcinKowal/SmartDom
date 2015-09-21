using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartDom.Service.Interface;

namespace SmartDom.Service.MediaAdapters
{
    public abstract class MediaAdapter<T> : IDisposable
    {
        public abstract T Media {get;}
        public abstract void Initialize();
        public abstract void Dispose();
    }
    
    public class MediaAdapter : MediaAdapter<SerialPort>
    {
        private SerialPort serialPort;
        public IConfigurationRepository Configuration { get; set; }
        
        public MediaAdapter(IConfigurationRepository configuration)
        {
            this.Configuration = configuration;
            
            serialPort = new SerialPort
            {
                BaudRate = Configuration.GetConfigurationValue<int>("server", "Baud"),
                DataBits = Configuration.GetConfigurationValue<int>("server", "DataBit"),
                Parity = Configuration.GetConfigurationValue<Parity>("server", "ParityBit"),
                StopBits = Configuration.GetConfigurationValue<StopBits>("server", "StopBit"),
                PortName = Configuration.GetConfigurationValue<string>("server", "SerialPort"),
                ReadTimeout = Configuration.GetConfigurationValue<int>("server", "ReadTimeout"),
                WriteTimeout = Configuration.GetConfigurationValue<int>("server", "WriteTimeout"),
            };
        }
        
        public override SerialPort Media
        {
            get {   return serialPort; }
        }

        public override void Initialize()
        {
            if (!serialPort.IsOpen)
                serialPort.Open();
        }

        public override void Dispose()
        {
            if (serialPort != null)
                serialPort.Dispose();
        }
    }
}
