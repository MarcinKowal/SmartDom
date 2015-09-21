//
// SmartDom
// SmartDom.Service.Interface.Messages
// AddDeviceRequest.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
// 

using SmartDom.Service.Interface.Models;

namespace SmartDom.Service.Interface.Messages
{
    using ServiceStack.ServiceHost;

    [Route("/device", "POST")]
    public class AddDeviceRequest : IReturnVoid
    {
        public Device DeviceItem { get; set; }
    }
}
