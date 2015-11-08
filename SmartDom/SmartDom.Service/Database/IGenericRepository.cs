//
// SmartDom
// SmartDom.Service
// IGenericRepository.cs
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

    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Exists asynchronously
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        Task<bool> ExistAsync(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Gets all items of T asynchronously
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetAllAsync();

        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> query);

        Task InsertAsync(T item);
    }
}
