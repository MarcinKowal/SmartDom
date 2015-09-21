//
// SmartDom
// SmartDom.Host
// HostFactory.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
// 

namespace SmartDom.Host
{
    using ServiceStack.WebHost.Endpoints;
    using ServiceStack.WebHost.Endpoints.Support;

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
