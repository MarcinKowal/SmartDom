// SmartDom
// SmartDom.Service.Interface
// AddDeviceResponse.cs
//  
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
//  

namespace SmartDom.Service.Interface.Messages
{
    using ServiceStack;

    using Models;

    public class AddDeviceResponse : IResponse<Device>
    {
        public ResponseStatus ResponseStatus { get; set; }

        public Device Result { get; set; }
    }
}