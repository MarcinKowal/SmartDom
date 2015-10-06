//
// SmartDom
// SmartDom.Service.Interface
// GetDevicesResponse.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
// 

namespace SmartDom.Service.Interface.Messages
{
    using ServiceStack;
    using System.Collections.Generic;
    using Models;


    public class GetDevicesResponse : IResponse<IList<Device>>
    {
        public IList<Device> Result { get; set; }
        public ResponseStatus ResponseStatus { get; set; } //Automatic exception handling
    }
}
