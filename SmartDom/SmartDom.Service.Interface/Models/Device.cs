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
    using ServiceStack.DataAnnotations;
    public class Device
    {
        [PrimaryKey]
        public byte Id { get; set; }
        public DeviceState State { get; set; }
        public DeviceType Type { get; set; }
        public DeviceSubtype Subtype { get; set; }
        public ushort TotalErrors { get; set; }
        public ushort LastRequestCode { get; set; }

        public override bool Equals(object obj)
        {
            var device = obj as Device;
            return device != null && this.Id == device.Id;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
