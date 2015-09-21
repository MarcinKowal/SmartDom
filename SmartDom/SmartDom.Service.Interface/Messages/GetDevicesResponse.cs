//
// SmartDom
// SmartDom.Service.Interface
// GetDevicesResponse.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
// 

using SmartDom.Service.Interface.Models;

namespace SmartDom.Service.Interface.Messages
{
    using System.Collections.Generic;
    using ServiceStack.ServiceInterface.ServiceModel;

    public class GetDevicesResponse : IResponse<IList<Device>>
    {
        public IList<Device> Result { get; set; }
        public ResponseStatus ResponseStatus { get; set; } //Automatic exception handling
    }
}
