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
    using System.Collections.Generic;
    using System.Threading.Tasks;


    // Mono issue 
    // https://forums.xamarin.com/discussion/15509/error-getting-response-stream-readdone2-receivefailure
    // upgrade service stack required to install ServiceStack.HttpClient


    public class Client : IClient
    {
        private readonly IRestClient restClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        /// <param name="restClient"></param>
        /// clientFactory
        internal Client(IRestClient restClient)
        {
            this.restClient = restClient;
        }

        /// <summary>
        /// Adds the device.
        /// </summary>
        /// <param name="device">The device.</param>
        public void AddDevice(Device device)
        {
            var request = new AddDeviceRequest { DeviceItem = device };
            restClient.Post(request);
        }

        /// <summary>
        /// Gets the device based on its id
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns></returns>
        public Device GetDevice(byte deviceId)
        {
            var request = new GetDeviceRequest { Id = deviceId };
            return GetDevice(request);
        }

        public async Task<Device> GetDeviceAsync(byte deviceId)
        {
            var request = new GetDeviceRequest { Id = deviceId };
            return await Task<Device>.Factory.StartNew(() => GetDevice(request));
        }
        
        /// <summary>
        /// Gets the all devices.
        /// </summary>
        /// <returns></returns>
        public IList<Device> GetDevices()
        {
            var request = new GetDevicesRequest();
            return GetDevices(request);
        }

        public async Task<IList<Device>> GetDevicesAsync()
        {
            return await Task<IList<Device>>.Factory.StartNew(() => GetDevices(new GetDevicesRequest()));
        }

        /// <summary>
        /// Removes the device.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        public void RemoveDevice(byte deviceId)
        {
            var request = new RemoveDeviceRequest { Id = deviceId };
            restClient.Delete(request);
        }

        /// <summary>
        /// Sets the state of the device.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="deviceState">State of the device.</param>
        public void SetDeviceState(byte deviceId, DeviceState deviceState)
        {
            var request = new SetDeviceStateRequest { Id = deviceId, State = deviceState };
            restClient.Put(request);
        }

        public async Task SetDeviceStateAsync(byte deviceId, DeviceState deviceState)
        {
            await Task.Factory.StartNew(() => restClient.Put(new SetDeviceStateRequest { Id = deviceId, State = deviceState }));
        }

        /// <summary>
        /// Gets the state of the device.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns></returns>
        public DeviceState GetDeviceState(byte deviceId)
        {
            return GetDeviceState(new GetDeviceRequest { Id = deviceId });
        }

        public async Task<DeviceState> GetDeviceStateAsync(byte deviceId)
        {
            return await Task<DeviceState>.Factory.StartNew(() => GetDeviceState(new GetDeviceRequest { Id = deviceId }));
        }
        
        private DeviceState GetDeviceState(GetDeviceRequest request)
        {
            var response = restClient.Get(request);
            return response.Result.State;
        }

        private IList<Device> GetDevices(IReturn<GetDevicesResponse> request)
        {
            var response = restClient.Get(request);
            return response != null ? response.Result : null;
        }

        private Device GetDevice(IReturn<GetDeviceResponse> request)
        {
            var response = restClient.Get(request);
            return response != null ? response.Result : null;
        }
    }
}
