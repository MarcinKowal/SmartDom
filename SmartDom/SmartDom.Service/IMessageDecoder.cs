#region Copyrights
//
// SmartDom
// SmartDom.Service
// IMessageDecoder.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
//  
#endregion

namespace SmartDom.Service
{
    using Interface.Models;

    public interface IMessageDecoder
    {
       Device Decode(ushort[] message);
    }
}
