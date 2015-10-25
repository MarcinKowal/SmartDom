// SmartDom
// SmartDom.Service
// DeviceDbRepository.cs
//  
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
//  

namespace SmartDom.Service.Database
{
    using ServiceStack.Data;
    using ServiceStack.Logging;
    using Interface.Models;

    using ServiceStack.OrmLite;

    public sealed class DeviceDbRepository : GenericDbRepository<Device>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceDbRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The database connection factory.</param>
        /// <param name="ormWrapper">The orm wrapper.</param>
        /// <param name="logger">The logger.</param>
        public DeviceDbRepository(IDbConnectionFactory dbConnectionFactory, IOrmWrapper ormWrapper,
            ILog logger)
            : base(dbConnectionFactory, ormWrapper, logger)
        {}

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize()
        {
            using (var db = this.DbConnectionFactory.Open())
            {
                if (!this.OrmWrapper.TableExists<Device>(db))
                {
                    this.OrmWrapper.CreateTable<Device>(db);
                }
            }
        }
    }
}