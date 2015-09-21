﻿//
// SmartDom
// SmartDom.Service.Interface.Messages
// RemoveDeviceRequest.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
// 

namespace SmartDom.Service.Interface.Messages
{
    using ServiceStack.ServiceHost;

    [Route("/device/{Id}", "DELETE")]
    public class RemoveDeviceRequest : IReturnVoid
    {
        public byte Id { get; set; }
    }
}
