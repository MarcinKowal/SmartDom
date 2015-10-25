#region Copyrights
//
// SmartDom
// SmartDom.Service
// MessageDecoder.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
//  
#endregion

namespace SmartDom.Service
{
    using Interface.Models;
    using System;

    public class MessageDecoder : IMessageDecoder
    {
        private const ushort ModbusBroadcastAddress = 0x00;

        /// <summary>
        /// Decodes the specified raw message.
        /// </summary>
        /// <param name="rawMessage">The raw message.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">rawMes sage</exception>
        /// <exception cref="System.ArgumentException">Message is too short</exception>
        public Device Decode(ushort[] rawMessage)
        {
            if (null == rawMessage)
                throw new ArgumentNullException("rawMessage");

            if (rawMessage.Length != Registry.TOTAL_REGS_SIZE)
                throw new ArgumentException("Message is too short");

            return new Device
            {
                Id = DecodeDeviceId(rawMessage[Registry.ID_ADDR]),
                Type = DecodeDeviceType(rawMessage[Registry.TYPE_ADDR]),
                Subtype = DecodeDeviceSubtype(rawMessage[Registry.SUBTYPE_ADDR]),
                State = DecodeDeviceState(rawMessage[Registry.STATE_ADDR]),
                TotalErrors = rawMessage[Registry.TOTAL_ERRORS_ADDR],
            };
        }

        /// <summary>
        /// Decodes the device identifier.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Decoded device id is reserved for broadcasting</exception>
        private static byte DecodeDeviceId(ushort deviceId)
        {
            if (deviceId == ModbusBroadcastAddress)
                throw new ArgumentException("Decoded device id is reserved for broadcasting");
            return (byte)deviceId;
        }

        /// <summary>
        /// Decodes the state of the device.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Unknown device state</exception>
        private static DeviceState DecodeDeviceState(ushort state)
        {
            if (!Enum.IsDefined(typeof(DeviceState), state))
                throw new ArgumentException("Unknown device state");
            else return (DeviceState)state;
        }

        /// <summary>
        /// Decodes the device subtype.
        /// </summary>
        /// <param name="subtype">The subtype.</param>
        /// <returns></returns>
        private static DeviceSubtype DecodeDeviceSubtype(ushort subtype)
        {
            return Enum.IsDefined(typeof(DeviceSubtype), subtype)
                ? (DeviceSubtype)subtype : DeviceSubtype.Unknown;
        }

        /// <summary>
        /// Decodes the type of the device.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        private static DeviceType DecodeDeviceType(ushort type)
        {
            return Enum.IsDefined(typeof(DeviceType), type)
                ? (DeviceType)type : DeviceType.Unknown;
        }
    }
}
