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

            var serverUri = Platform.IsMonoRuntime ? "http://192.168.1.106:1337/api/":"http://192.168.1.53:1337/api/";
            var host = hostFactory.Create();
            host.Init();
            host.Start(serverUri);
            Console.ReadKey();
        }
    }
}
