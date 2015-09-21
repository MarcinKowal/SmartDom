//
// SmartDom
// SmartDom.Client
// ClientFactory.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
// 

namespace SmartDom.Client
{
    using ServiceStack.Service;
    using ServiceStack.ServiceClient.Web;

    public class ClientFactory : IClientFactory
    {
        public IClient Create(string serverAddress, ClientFormat clientFormat, ClientCredentials clientCredentials)
        {
            var restClient = GetRestClient(serverAddress, clientFormat);
            restClient.SetCredentials(clientCredentials.Username, clientCredentials.Password);
            restClient.AlwaysSendBasicAuthHeader = true;
            return new Client(restClient);
        }

        private static ServiceClientBase GetRestClient(string serverAddress, ClientFormat clientFormat)
        {
            switch (clientFormat)
            {
                case ClientFormat.Xml:
                    {
                        return new XmlServiceClient(serverAddress);
                    }
                default:
                    {
                        return new JsonServiceClient(serverAddress);
                    }
            }
        }
    }
}
