using System;
using System.IO.Ports;
using SmartDom.Service.Interface;
namespace SmartDom.Service
{
    public interface IModbusMasterFactory
    {
        IModbusMasterAdapter Create(IMediaAdapter<SerialPort> media);
    }
}
