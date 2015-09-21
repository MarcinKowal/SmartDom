//
// SmartDom
// SmartDom.Service
// MediaAdapterAbstractFactory.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
// 

namespace SmartDom.Service.MediaAdapters
{
    public abstract class MediaAdapterAbstractFactory<T>
    {
        public abstract MediaAbstractAdapter<T> Create();
    }
}
