﻿//
// SmartDom
// SmartDom.Service.Interface.Messages
// AddDeviceRequest.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
// 


namespace SmartDom.Service.Interface.Messages
{
    using ServiceStack;
    using Models;

    [Route("/device", "POST")]
    public class AddDeviceRequest : IReturn<AddDeviceResponse>
    {
        public byte DeviceId { get; set; }
        public DeviceType DeviceType { get; set; } 
        public DeviceSubtype DeviceSubType { get; set; }
    }
}
