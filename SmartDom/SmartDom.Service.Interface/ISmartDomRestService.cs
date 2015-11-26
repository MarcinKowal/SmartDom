//
// SmartDom
// SmartDom.Service.Interface
// ISmartDomRestService.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
// 

namespace SmartDom.Service.Interface
{
    using Messages;
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISmartDomRestService
    {
        /// <summary>
        /// Handles asynchronously HTTP GET with specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        Task<IResponse<Device>> Get(GetDeviceRequest request);
       
        /// <summary>
        /// Handles asynchronously HTTP GET with specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        Task<IResponse<IList<Device>>> Get(GetDevicesRequest request);
      
        /// <summary>
        /// Handles asynchronously HTTP POST with specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        Task<IResponse<Device>> Post(AddDeviceRequest request);
     
        /// <summary>
        /// Handles asynchronously HTTP DELETE with specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        Task Delete(RemoveDeviceRequest request);
      
        /// <summary>
        /// Handles asynchronously HTTP PUT with specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        Task Put(SetDeviceStateRequest request);
    }
}
