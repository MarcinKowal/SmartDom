#region Copyrights
//
// SmartDom
// SmartDom.Host
// UnityContainerAdapter.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
//  
#endregion
namespace SmartDom.Host
{
    using Microsoft.Practices.Unity;
    using ServiceStack.Configuration;

    /// <summary>
    /// IContainerAdapter for the unity IoC.
    /// </summary>
    public class UnityContainerAdapter : IContainerAdapter
    {
        private readonly IUnityContainer container;

        public UnityContainerAdapter(IUnityContainer container)
        {
            this.container = container;
        }

        public T Resolve<T>()
        {
            return container.Resolve<T>();
        }

        public T TryResolve<T>()
        {
            return container.IsRegistered(typeof(T))
                ? (T)container.Resolve(typeof(T)) : default(T);
        }
    }
}

