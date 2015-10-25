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
    using ServiceStack.Logging;
    using Service;
    using Service.Interface;
    using Service.MediaAdapters;
    using Service.DeviceLayers;
    using Service.ModbusAdapters;
    using ServiceStack;
    using ServiceStack.Auth;
    using ServiceStack.Caching;
    using ServiceStack.Data;
    using ServiceStack.OrmLite;
    using IoC;
    using Service.Database;
    using ServiceStack.OrmLite.Sqlite;

    using SmartDom.Service.Interface.Models;

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
        /// <param name="urlBase">The URL.</param>
        public override ServiceStackHost Start(string urlBase)
        {
            logger.Info("Starting SmartDom host created: " + Environment.NewLine +  urlBase);
            logger.Info("Runtime platform: " + Platform.RuntimePlatform);
            return base.Start(urlBase);
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
            var unityContainer = new UnityContainer()
              .AddNewExtension<BuildTracking>()
              .AddNewExtension<LogCreation>();
            
            var userRepository = new InMemoryAuthRepository();

            Plugins.Add(new AuthFeature(() => new AuthUserSession(),
              new IAuthProvider[] { new BasicAuthProvider()}));

            Plugins.Add(new RegistrationFeature());
           
            unityContainer.RegisterInstance<ICacheClient>(new MemoryCacheClient());
            unityContainer.RegisterInstance<IUserAuthRepository>(userRepository);

            unityContainer.RegisterInstance<IDbConnectionFactory>(new OrmLiteConnectionFactory("~/Database/SmartDom.db".MapServerPath(), 
                SqliteOrmLiteDialectProvider.Instance));

            unityContainer.RegisterType<IOrmWrapper, OrmWrapper>();
            unityContainer.RegisterType<IConfigurationRepository, AppConfigRepository>();
            unityContainer.RegisterType<MediaAdapterAbstractFactory<SerialPort>, SerialPortAdapterFactory>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<ModbusMasterAbstractFactory<SerialPort>, RtuSerialModbusMasterFactory>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<IGenericRepository<Device>, DeviceDbRepository>(new ContainerControlledLifetimeManager(),
                new InjectionMethod("Initialize"));
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
