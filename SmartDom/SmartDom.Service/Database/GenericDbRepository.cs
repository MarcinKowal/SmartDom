// SmartDom
// SmartDom.Service
// GenericDbRepository.cs
//  
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
//  

namespace SmartDom.Service.Database
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using ServiceStack.Data;
    using ServiceStack.Logging;

    public abstract class GenericDbRepository<T> : IGenericRepository<T>
        where T : class
    {
        protected readonly IDbConnectionFactory DbConnectionFactory;
        protected readonly ILog Logger;
        protected readonly IOrmWrapper OrmWrapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericDbRepository{T}"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The database connection factory.</param>
        /// <param name="ormWrapper">The orm wrapper.</param>
        /// <param name="logger">The logger.</param>
        protected GenericDbRepository(IDbConnectionFactory dbConnectionFactory, IOrmWrapper ormWrapper, ILog logger)
        {
            this.DbConnectionFactory = dbConnectionFactory;
            this.OrmWrapper = ormWrapper;
            this.Logger = logger;
        }

        /// <summary>
        /// Exists asynchronously.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public virtual async Task<bool> ExistAsync(Expression<Func<T, bool>> expression)
        {
            try
            {
                using (var connection = this.DbConnectionFactory.OpenDbConnection())
                {
                    return await this.OrmWrapper.ExistsAsync(connection, expression);
                }
            }
            catch (Exception e)
            {
                this.Logger.Error(e);
            }
            return false;
        }

        /// <summary>
        /// Gets all items of T asynchronously
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<T>> GetAllAsync()
        {
            try
            {
                using (var connection = this.DbConnectionFactory.OpenDbConnection())
                {
                    return await this.OrmWrapper.SelectAsync<T>(connection);
                }
            }
            catch (Exception e)
            {
                this.Logger.Error(e);
            }
            return new List<T>();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> query)
        {
            try
            {
                using (var connection = this.DbConnectionFactory.OpenDbConnection())
                {
                    return await this.OrmWrapper.SelectAsync(connection, query);
                }
            }
            catch (Exception e)
            {
                this.Logger.Error(e);
            }
            return new List<T>();
        }

        public async Task InsertAsync(T item)
        {
            try
            {
                using (var connection = this.DbConnectionFactory.OpenDbConnection())
                {
                    await this.OrmWrapper.InsertAsync(connection, item);
                }
            }
            catch (Exception e)
            {
                this.Logger.Error(e);
            }
        }

        public async Task<int> DeleteAsync(Expression<Func<T, bool>> filterExpression)
        {
            try
            {
                using (var connection = this.DbConnectionFactory.OpenDbConnection())
                {
                    return await this.OrmWrapper.DeleteAsync(connection, filterExpression);
                }
            }
            catch (Exception e)
            {
                this.Logger.Error(e);
            }
            return 0;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public abstract void Initialize();
    }
}