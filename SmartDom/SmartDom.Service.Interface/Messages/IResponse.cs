//
// SmartDom
// SmartDom.Service.Interface
// IResponse.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
// 

namespace SmartDom.Service.Interface.Messages
{
    using ServiceStack.ServiceInterface.ServiceModel;

    public interface IResponse<T>
    {
        ResponseStatus ResponseStatus { get; set; } //Automatic exception handling
        T Result { get; set; }    
    }
}