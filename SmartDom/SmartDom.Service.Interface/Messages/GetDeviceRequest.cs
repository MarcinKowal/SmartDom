//
// SmartDom
// SmartDom.Service.Interface.Messages
// GetDeviceRequest.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
// 


namespace SmartDom.Service.Interface.Messages
{
    using ServiceStack;

    [Route("/device/{Id}", Verbs = "GET")]
    public class GetDeviceRequest : IReturn<GetDeviceResponse>
    {
        public byte Id { get; set; }
    }
}
