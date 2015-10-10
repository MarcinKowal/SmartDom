//
// SmartDom
// SmartDom.Host
// Platform.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
// 
using System;

namespace SmartDom.Host
{
    public static class Platform
    {
        /// <summary>
        /// Gets the runtime platform.
        /// </summary>
        /// <value>
        /// The runtime platform.
        /// </value>
        public static string RuntimePlatform
        {
            get { return Type.GetType("Mono.Runtime") != null ? "Mono VM" : "Windows OS"; }
        }

        public static bool IsMonoRuntime
        {
            get { return Type.GetType("Mono.Runtime") != null ? true : false; }
        }

        public static string ServerUri
        {
            get
            {
                return IsMonoRuntime ?
                    "http://192.168.1.106:1337/api/" 
                    : "http://192.168.1.53:1337/api/";
            }
        }
    }
}
