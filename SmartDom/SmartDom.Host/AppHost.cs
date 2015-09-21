//
// SmartDom
// SmartDom.Host
// AppHost.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
// 

namespace SmartDom.Host
{
    using System;
    using System.IO.Ports;
    using Funq;
    using Microsoft.Practices.Unity;
    using ServiceStack.CacheAccess;
    using ServiceStack.CacheAccess.Providers;
    using ServiceStack.ContainerAdapter.Unity;
    using ServiceStack.Logging;
    using ServiceStack.ServiceInterface;
    using ServiceStack.ServiceInterface.Auth;
    using ServiceStack.WebHost.Endpoints;
    using Service;
    using Service.Interface;
    using Service.MediaAdapters;
    using Service.DeviceLayers;
    using Service.ModbusAdapters;


    public class AppHost : AppHostHttpListenerBase
    {
        private static ILog logger;
        
        public AppHost()
            : base("SmartDom.Host", typeof (SmartDomService).Assembly)
        {

            logger = LogManager.GetLogger(GetType());
        }

        
        /// <summary>
        /// Starts the process host with specified url.
        /// </summary>
        /// <param name="urlBase">The URL base.</param>
        public override void Start(string urlBase)
        {
            logger.Info("Starting SmartDom host created: " + Environment.NewLine + urlBase);
            logger.Info("Runtime platform: " + Platform.RuntimePlatform);
            base.Start(urlBase);
        }

        /// <summary>
        /// Shut down the process host
        /// </summary>
        public override void Stop()
        {
            logger.Info("SmartDom host is going shutdown"); 
            base.Stop();
        }

        public override void Configure(Container container)
        {
            var unityContainer = new UnityContainer();
            var userRepository = new InMemoryAuthRepository();

            Plugins.Add(new AuthFeature(() => new AuthUserSession(),
              new IAuthProvider[] { new BasicAuthProvider()}));

            Plugins.Add(new RegistrationFeature());
           
            unityContainer.RegisterInstance<ICacheClient>(new MemoryCacheClient());
            unityContainer.RegisterInstance<IUserAuthRepository>(userRepository);


            unityContainer.RegisterType<IConfigurationRepository, AppConfigRepository>();
            unityContainer.RegisterType<MediaAdapterAbstractFactory<SerialPort>, SerialPortAdapterFactory>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<ModbusMasterAbstractFactory<SerialPort>, RtuSerialModbusMasterFactory>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<IDeviceAccessLayer, SerialAccessLayer>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<IMessageDecoder, MessageDecoder>();
            unityContainer.RegisterType<IDeviceManager, DeviceManager>();
            
            var unityAdapter = new UnityContainerAdapter(unityContainer);
            container.Adapter = unityAdapter;



            //Add a user for testing purposes
            string hash;
            string salt;
            new SaltedHash().GetHashAndSaltString("test", out hash, out salt);
            
            userRepository.CreateUserAuth(new UserAuth
            {
                Id = 1,
                DisplayName = "DisplayName",
                Email = "as@if.com",
                UserName = "test",
                FirstName = "FirstName",
                LastName = "LastName",
                PasswordHash = hash,
                Salt = salt,
            }, "test");


        }
    }
}
