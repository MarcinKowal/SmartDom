// SmartDom
// SmartDom.Service
// OrmWrapper.cs
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

    using ServiceStack.OrmLite;
    using ServiceStack.OrmLite.Dapper;
    using ServiceStack.Text;

    public class OrmWrapper : IOrmWrapper
    {
        /// <summary>
        /// Checks if table exists.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection">The database connection.</param>
        /// <returns></returns>
        public bool TableExists<T>(IDbConnection dbConnection)
        {
            return dbConnection.TableExists<T>();
        }

        public async Task<bool> ExistsAsync<T>(IDbConnection dbConnection, Expression<Func<T, bool>> expression)
        {
            return await dbConnection.ExistsAsync(expression);
        }

        /// <summary>
        /// Creates the table.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection">The database connection.</param>
        /// <param name="overwrite">if set to <c>true</c> [overwrite].</param>
        public void CreateTable<T>(IDbConnection dbConnection, bool overwrite = false)
        {
            dbConnection.CreateTable<T>(overwrite);
        }


        /// <summary>
        /// Selects item asynchronously
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection">The database connection.</param>
        /// <returns></returns>
        public async Task<List<T>> SelectAsync<T>(IDbConnection dbConnection)
        {
            return await dbConnection.SelectAsync<T>();
        }

        public async Task<List<T>> SelectAsync<T>(IDbConnection dbConnection, Expression<Func<T, bool>> query)
        {
            return await dbConnection.SelectAsync(query);
        }

        /// <summary>
        /// Inserts items asynchronously
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection">The database connection.</param>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public async Task InsertAsync<T>(IDbConnection dbConnection, T item)
        {
            await dbConnection.InsertAsync(item);
        }

        /// <summary>
        /// Removes items asynchronously
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection">The database connection.</param>
        /// <param name="filterExpression">The filter expression.</param>
        /// <returns></returns>
        public async Task<int> DeleteAsync<T>(IDbConnection dbConnection, Expression<Func<T, bool>> filterExpression)
        {
            return await dbConnection.DeleteAsync(filterExpression);
        }
    }
}