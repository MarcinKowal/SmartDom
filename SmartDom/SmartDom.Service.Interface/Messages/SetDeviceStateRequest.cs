//
// SmartDom
// SmartDom.Service.Interface.Messages
// SetDeviceStateRequest.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
// 


namespace SmartDom.Service.Interface.Messages
{
    using ServiceStack;
    using Models;

    [Route("/device/{Id}/state", "PUT")]
    public class SetDeviceStateRequest : IReturnVoid
    {
        public byte Id { get; set; }
        public DeviceState State { get; set; }
    }
}
