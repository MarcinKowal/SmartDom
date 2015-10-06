//
// SmartDom
// SmartDom.Host
// HostFactory.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
// 

using ServiceStack;
using ServiceStack.Host.HttpListener;

namespace SmartDom.Host
{
    public interface IHostFactory<T> where T : HttpListenerBase
    {
        T Create();
    }

    public class HostFactory : IHostFactory<AppHostHttpListenerBase>
    {
        public AppHostHttpListenerBase Create()
        { 
            return new AppHost();
        }
    }
}
