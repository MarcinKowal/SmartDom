//
// SmartDom
// SmartDom.Service.Interface.DataModel
// DeviceException.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
// 

using System;

namespace SmartDom.Service.Interface.Models
{
    [Serializable]
    public class DeviceException : Exception
    {
        public DeviceException() { }
        public DeviceException(string message) : base(message) { }
        public DeviceException(string message, Exception inner) : base(message, inner) { }
        protected DeviceException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}