//
// SmartDom
// SmartDom.Service.Interface
// GetDeviceResponse.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
// 

using ServiceStack;
using SmartDom.Service.Interface.Models;

namespace SmartDom.Service.Interface.Messages
{
    public class GetDeviceResponse : IResponse<Device>
    {
        public ResponseStatus ResponseStatus { get; set; }
        public Device Result { get; set; }
    }
}