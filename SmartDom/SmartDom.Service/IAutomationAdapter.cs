using System;
using SmartDom.DataModel;
using SmartDom.Modbus;
namespace SmartDom.Service
{
    public interface IAutomationAdapter
    {
        Device GetDevice(byte deviceId);
    }
}
