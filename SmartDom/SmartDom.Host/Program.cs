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
    using System;
    using ServiceStack.Logging;
    using ServiceStack.Logging.NLogger;
    using SmartDom.Service;

    class Program
    {
        static void Main(string[] args)
        {
            LogManager.LogFactory = new NLogFactory();
            var hostFactory = new HostFactory();

            var host = hostFactory.Create();
            host.Init();
            host.Start(Platform.ServerUri);
            Console.ReadKey();
        }
    }
}
