using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using Modbus.Device;
using SmartDom.Configuration;
using SmartDom.DataModel;
using SmartDom.Modbus;

using SmartDom.Modbus.Messages;

namespace SmartDom.Service
{
    public class AutomationAdapter : IAutomationAdapter
    {
        private readonly IModbusMasterFactory modbusMasterFactory;
        private readonly IServerConfiguration configuration;
        private readonly IMessageDecoder messageDecoder;
        private readonly IMessageEncoder messageEncoder;

        private static IModbusSerialMaster modbusMaster;

        public AutomationAdapter(IModbusMasterFactory modbusMasterFactory,
            IServerConfiguration configuration, IMessageEncoder messageEncoder,
            IMessageDecoder messageDecoder)
        {
            if (null == modbusMasterFactory)
                throw new ArgumentNullException("modbusMasterFactory");
            if (null == configuration)
                throw new ArgumentNullException("serialConfiguration");
            if (null == messageEncoder)
                throw new ArgumentNullException("messageEncoder");
            if (null == messageDecoder)
                throw new ArgumentNullException("messageDecoder");

            this.modbusMasterFactory = modbusMasterFactory;
            this.configuration = configuration;
            this.messageEncoder = messageEncoder;
            this.messageDecoder = messageDecoder;
        }


        public DeviceInfo GetDeviceInfo(byte deviceId)
        {
            var encodedRequest =
               messageEncoder.Encode(new SlaveInfoRequest());

            var readData = MasterInstance.ReadWriteMultipleRegisters(deviceId,
                Registry.TYPE_ADDR,
                Registry.TOTAL_REGS_SIZE,
                Registry.LAST_REQUEST_ADDR,
                encodedRequest);

            var decodedResponse = messageDecoder.Decode(readData);
            
            if (decodedResponse.Code.Equals(MessageCode.SlaveInfoResponse))
                return ((SlaveInfoResponse)decodedResponse).DeviceInfo;
            return null;
        }

        private IModbusSerialMaster MasterInstance
        {
            get 
            {
                if (modbusMaster != null)
                    return modbusMaster;

                var serialPort = GetSerialPort();
                serialPort.Open();
                modbusMaster = modbusMasterFactory.Create(serialPort);
                return modbusMaster;
            }
        }

        private SerialPort GetSerialPort()
        {
            
            var serialPort = new SerialPort
            {
                ReadTimeout = 1000,
                WriteTimeout = 1000,
                PortName = configuration.PortName,
                BaudRate = configuration.BaudRate,
                DataBits = configuration.DataBits,
                Parity = Parity.None,
                StopBits = StopBits.Two,
            };
            return serialPort;
        }
    }
}
