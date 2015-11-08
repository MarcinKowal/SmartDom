//
// SmartDom
// SmartDom.Client
// Client.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
// 

namespace SmartDom.Client
{
    using Service.Interface.Messages;
    using Service.Interface.Models;
    using ServiceStack;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;


    // Mono issue 
    // https://forums.xamarin.com/discussion/15509/error-getting-response-stream-readdone2-receivefailure
    // upgrade service stack required to install ServiceStack.HttpClient


    public class Client : IClient
    {
        private readonly IRestClientAsync restClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        /// <param name="restClient"></param>
        /// clientFactory
        internal Client(IRestClientAsync restClient)
        {
            this.restClient = restClient;
        }

        public async Task<Device> GetDeviceAsync(byte deviceId)
        {
            var request = new GetDeviceRequest { Id = deviceId };
            var response = await this.restClient.GetAsync(request);
            return response != null ? response.Result : null;
        }
        
        public async Task<IList<Device>> GetDevicesAsync()
        {
           var response = await this.restClient.GetAsync(new GetDevicesRequest());
           return response != null ? response.Result : null;     
        }

        public async Task SetDeviceStateAsync(byte deviceId, DeviceState deviceState)
        {
            await this.restClient.PutAsync(
                new SetDeviceStateRequest
                { Id = deviceId, State = deviceState });
        }

        public async Task<DeviceState> GetDeviceStateAsync(byte deviceId)
        {
            var response = await this.restClient.GetAsync(new GetDeviceRequest { Id = deviceId });
            return response.Result.State;
        }

        /// <summary>
        /// Removes the device.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        public async Task RemoveDeviceAsync(byte deviceId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds the device.
        /// </summary>
        /// <param name="device">The device.</param>
        public async Task AddDeviceAsync(Device device)
        {
            var request = new AddDeviceRequest { Device = device };
            await this.restClient.PostAsync(request);
        }
    }
}
