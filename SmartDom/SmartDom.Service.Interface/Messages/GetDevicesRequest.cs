//
// SmartDom
// SmartDom.Service.Interface
// GetDevicesRequest.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
// 

namespace SmartDom.Service.Interface.Messages
{
    using ServiceStack.ServiceHost;

    [Route("/devices", "GET")]
    public class GetDevicesRequest : IReturn<GetDevicesResponse>
    {
    }
}
