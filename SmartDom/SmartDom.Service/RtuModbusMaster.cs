using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modbus.Device;
using SmartDom.Service.Interface;
using SmartDom.Service.MediaAdapters;

namespace SmartDom.Service
{
    public class RtuModbusMaster : IModbusMasterAdapter
    {
        private readonly ModbusMaster modbus;

        public RtuModbusMaster(MediaAdapter<SerialPort> mediaAdapter)
        {
            modbus = ModbusSerialMaster.CreateRtu(mediaAdapter.Media);
        }
        
        public ushort[] ReadHoldingRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            return modbus.ReadHoldingRegisters(slaveAddress, startAddress, numberOfPoints);
        }

        public async Task<ushort[]> ReadHoldingRegistersAsync(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            return await modbus.ReadHoldingRegistersAsync(slaveAddress, startAddress, numberOfPoints);    
        }

        public void WriteMultipleRegisters(byte slaveAddress, ushort startAddress, ushort[] data)
        {
            modbus.WriteMultipleRegisters(slaveAddress, startAddress, data);
        }

        public async Task WriteMultipleRegistersAsync(byte slaveAddress, ushort startAddress, ushort[] data)
        {
            await modbus.WriteMultipleRegistersAsync(slaveAddress, startAddress, data);
        }

        public void Dispose()
        {
            if (modbus != null)
                modbus.Dispose();
        }
    }
}
