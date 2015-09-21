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
    }
}
