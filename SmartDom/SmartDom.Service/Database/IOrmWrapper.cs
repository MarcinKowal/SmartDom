// SmartDom
// SmartDom.Service
// IOrmWrapper.cs
//  
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
//  

namespace SmartDom.Service.Database
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IOrmWrapper
    {
        /// <summary>
        /// Checks if table exists.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection">The database connection.</param>
        /// <returns></returns>
        bool TableExists<T>(IDbConnection dbConnection);

        Task<bool> ExistsAsync<T>(IDbConnection dbConnection, Expression<Func<T, bool>> expression);

        /// <summary>
        /// Creates the table.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection">The database connection.</param>
        /// <param name="overwrite">if set to <c>true</c> [overwrite].</param>
        void CreateTable<T>(IDbConnection dbConnection, bool overwrite = false);

        /// <summary>
        /// Inserts items asynchronously
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection">The database connection.</param>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        Task InsertAsync<T>(IDbConnection dbConnection, T item);

        Task<List<T>> SelectAsync<T>(IDbConnection dbConnection, Expression<Func<T, bool>> query);

        /// <summary>
        /// Selects item asynchronously
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection">The database connection.</param>
        /// <returns></returns>
        Task<List<T>> SelectAsync<T>(IDbConnection dbConnection);
    }
}