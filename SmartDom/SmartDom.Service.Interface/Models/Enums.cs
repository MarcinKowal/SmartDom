//
// SmartDom
// SmartDom.Service.Interface.DataModel
// Enums.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
// 

namespace SmartDom.Service.Interface.Models
{
    public enum DeviceType : ushort
    {
        Unknown = 0,
        Socket = 1,
        Switch = 2,
    }

    public enum DeviceSubtype : ushort
    {
        Unknown = 0,
        Digital,
    }

    public enum DeviceState : ushort
    {
        Unknown = 0,
        Disabled = 1,
        Enabled = 2,
    }
}
