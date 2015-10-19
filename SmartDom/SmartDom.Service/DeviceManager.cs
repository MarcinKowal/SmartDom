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
    using System.Threading.Tasks;

    public class DeviceManager : IDeviceManager
    {
        private readonly IDeviceAccessLayer deviceAccessLayer;
        private readonly IMessageDecoder messageDecoder;
        private readonly ILog logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceManager"/> class.
        /// </summary>
        /// <param name="deviceAccessLayer">The device access layer.</param>
        /// <param name="messageDecoder">The message decoder.</param>
        public DeviceManager(IDeviceAccessLayer deviceAccessLayer, IMessageDecoder messageDecoder, ILog logger)
        {
            this.deviceAccessLayer = deviceAccessLayer;
            this.messageDecoder = messageDecoder;
            this.logger = logger;
        }
        /// <summary>
        /// Gets the device.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns></returns>
        public async Task<Device> GetDeviceAsync(byte deviceId)
        {
            ushort[] receivedData;
            try
            {
                receivedData = await deviceAccessLayer.ReadFromDeviceAsync(deviceId, Registry.ID_ADDR, Registry.TOTAL_REGS_SIZE);
            }
            catch (SlaveException e)
            {
                logger.Error(string.Format("Unable to retrieve device {0}. Exception occured {1}", deviceId, e));
                throw new DeviceException("Unable to communicate with device with ID= " + deviceId);
            }
            catch (TimeoutException e)
            {
                logger.Error(string.Format("Unable to retrieve device {0}. Exception occured {1}", deviceId, e));
                throw new DeviceException("Unable to communicate with device with ID= " + deviceId);
            }

            return messageDecoder.Decode(receivedData);
        }
        public async Task SetDeviceStateAsync(byte deviceId, DeviceState deviceState)
        {
            try
            {
                var data = new[] { (ushort)deviceState };
                await deviceAccessLayer.WriteToDeviceAsync(deviceId, Registry.STATE_ADDR, data);
            }
            catch (SlaveException e)
            {
                logger.Error(string.Format("Unable to set state of device {0} to {1}. Exception occured {2}", deviceId, deviceState, e));
                throw new DeviceException("Unable to communicate with device with ID= " + deviceId);
            }
            catch (TimeoutException e)
            {
                logger.Error(string.Format("Unable to set state of device {0} to {1}. Exception occured {2}", deviceId, deviceState, e));
                throw new DeviceException("Unable to communicate with device with ID= " + deviceId);
            }
        }
    }
}
