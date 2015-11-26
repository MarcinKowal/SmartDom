// SmartDom
// SmartDom.Service
// ResponseFactory.cs
//  
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
//  

namespace SmartDom.Service
{
    using Interface.Messages;

    public class ResponseFactory
    {
        public static T CreateResponse<T, TV>(TV device) where T : IResponse<TV>, new()
        {
            return new T { Result = device };
        }
    }
}