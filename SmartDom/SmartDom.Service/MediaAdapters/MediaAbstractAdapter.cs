//
// SmartDom
// SmartDom.Service
// MediaAbstractAdapter.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
// 

using System;

namespace SmartDom.Service.MediaAdapters
{
    public abstract class MediaAbstractAdapter<T> : IDisposable
    {
        public abstract T Media {get;}
        public abstract void Initialize();
        public abstract void Dispose();
    }
}