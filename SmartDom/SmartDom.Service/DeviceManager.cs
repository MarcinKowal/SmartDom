#region Copyrights
//
// SmartDom
// SmartDom.Service
// DeviceManager.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
//  
#endregion

namespace SmartDom.Service
{
    using DeviceLayers;
    using Interface.Models; 
    using Modbus;
    using ServiceStack.Logging;
    using System;

    public class DeviceManager : IDeviceManager
    {
        private readonly IDeviceAccessLayer deviceAccessLayer;
        private readonly IMessageDecoder messageDecoder;
        private static readonly ILog Logger = LogManager.GetLogger(typeof(DeviceManager));
      
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceManager"/> class.
        /// </summary>
        /// <param name="deviceAccessLayer">The device access layer.</param>
        /// <param name="messageDecoder">The message decoder.</param>
        public DeviceManager(IDeviceAccessLayer deviceAccessLayer, IMessageDecoder messageDecoder)
        {
            this.deviceAccessLayer = deviceAccessLayer;
            this.messageDecoder = messageDecoder;
        }

        /// <summary>
        /// Gets the device.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns></returns>
        public Device GetDevice(byte deviceId)
        {
            ushort[] receivedData;
            try
            {
                receivedData = deviceAccessLayer.ReadFromDevice(deviceId, Registry.ID_ADDR, Registry.TOTAL_REGS_SIZE);
            }
            catch (SlaveException e)
            {
                Logger.Error(string.Format("Unable to retrieve device {0}. Exception occured {1}", deviceId, e));
                throw new DeviceException("Unable to communicate with device with ID= " + deviceId);
            }
            catch (TimeoutException e)
            {
                Logger.Error(string.Format("Unable to retrieve device {0}. Exception occured {1}", deviceId, e));
                throw new DeviceException("Unable to communicate with device with ID= " + deviceId);
            }

            return messageDecoder.Decode(receivedData);
        }
        public void SetDeviceState(byte deviceId, DeviceState deviceState)
        {
            try
            {
                var data = new[] { (ushort)deviceState };
                deviceAccessLayer.WriteToDevice(deviceId, Registry.RW_DIG0_ADDR, data);
            }
            catch (SlaveException e)
            {
                Logger.Error(string.Format("Unable to set state of device {0} to {1}. Exception occured {2}", deviceId, deviceState, e));
                throw new DeviceException("Unable to communicate with device with ID= " + deviceId);
            }
            catch (TimeoutException e)
            {
                Logger.Error(string.Format("Unable to set state of device {0} to {1}. Exception occured {2}", deviceId, deviceState, e));
                throw new DeviceException("Unable to communicate with device with ID= " + deviceId);
            }
        }
    }
}
