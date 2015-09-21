using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDom.Service.Interface
{
    public interface IModbusMasterAdapter : IDisposable 
    {
       ushort[] ReadHoldingRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints);
       Task<ushort[]> ReadHoldingRegistersAsync(byte slaveAddress, ushort startAddress, ushort numberOfPoints);

       void WriteMultipleRegisters(byte slaveAddress, ushort startAddress, ushort[] data);
       Task WriteMultipleRegistersAsync(byte slaveAddress, ushort startAddress, ushort[] data);
    }
}
