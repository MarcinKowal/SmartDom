// SmartDom
// SmartDom.Service
// DeviceManager.cs
//  
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
//  

namespace SmartDom.Service
{
    using System;
    using System.Threading.Tasks;

    using ServiceStack.Logging;

    using SmartDom.Service.DeviceLayers;
    using SmartDom.Service.Interface.Models;

    public class DeviceManager : IDeviceManager
    {
        private readonly IDeviceAccessLayer deviceAccessLayer;

        private readonly ILog logger;

        private readonly IMessageDecoder messageDecoder;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DeviceManager" /> class.
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
        ///     Gets the device asynchronously
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns></returns>
        public async Task<Device> GetDeviceAsync(byte deviceId)
        {
            try
            {
                var receivedData =
                    await
                    this.deviceAccessLayer.ReadFromDeviceAsync(deviceId, Registry.ID_ADDR, Registry.TOTAL_REGS_SIZE);

                return this.messageDecoder.Decode(receivedData);
            }
            catch (Exception e)
            {
                this.logger.Error(string.Format("Unable to retrieve device {0}. Exception occured {1}", deviceId, e));
                throw new DeviceException("Unable to communicate with device with ID= " + deviceId);
            }
        }

        /// <summary>
        ///     Sets the device state asynchronously
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="deviceState">State of the device.</param>
        /// <returns></returns>
        /// <exception cref="DeviceException">Unable to communicate with device with ID=  + deviceId</exception>
        public async Task SetDeviceStateAsync(byte deviceId, DeviceState deviceState)
        {
            try
            {
                var data = new[] { (ushort)deviceState };
                await this.deviceAccessLayer.WriteToDeviceAsync(deviceId, Registry.STATE_ADDR, data);
            }
            catch (Exception e)
            {
                this.logger.Error(
                    string.Format(
                        "Unable to set state of device {0} to {1}. Exception occured {2}",
                        deviceId,
                        deviceState,
                        e));
                throw new DeviceException("Unable to communicate with device with ID= " + deviceId);
            }
        }
    }
}