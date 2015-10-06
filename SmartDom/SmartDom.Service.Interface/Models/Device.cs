//
// SmartDom
// SmartDom.Service.Interface.DataModel
// Device.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
// 

namespace SmartDom.Service.Interface.Models
{
    public class Device
    {
        public ushort Id { get; set; }
        public DeviceState State { get; set; }
        public DeviceType Type { get; set; }
        public DeviceSubtype Subtype { get; set; }
        public ushort TotalErrors { get; set; }
        public ushort LastRequestCode { get; set; }   
    }
}
