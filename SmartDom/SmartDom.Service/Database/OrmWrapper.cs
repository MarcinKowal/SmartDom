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


        public async Task<List<T>> SelectAsync<T>(IDbConnection dbConnection)
        {
            return await dbConnection.SelectAsync<T>();
        } 
    }
}