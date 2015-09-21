//
// SmartDom
// SmartDom.Client
// IAsyncClient.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
// 

namespace SmartDom.Client
{
    public interface IClientFactory
    {
        IClient Create(string serverAddress, ClientFormat clientFormat, ClientCredentials clientCredentials);
    }
}
