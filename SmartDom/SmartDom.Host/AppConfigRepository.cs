//
// SmartDom
// SmartDom.Host
// AppConfigRepository.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
// 

namespace SmartDom.Host
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Configuration;
    using SmartDom.Service.Interface;

    public class AppConfigRepository : IConfigurationRepository
    {
        public T GetConfigurationValue<T>(string sectionName, string key)
        {
            var section = ConfigurationManager.GetSection(sectionName) as NameValueCollection;
            var value = section[key];
            
            if (value == null)
            {
                throw new KeyNotFoundException("Key " + key + " not found.");
            }
          
            if (typeof(Enum).IsAssignableFrom(typeof(T)))
                return (T)Enum.Parse(typeof(T), value);
            return (T)Convert.ChangeType(value, typeof(T));
        }

        public T GetConfigurationValue<T>(string sectionName, string key, T defaultValue)
        {
            var section = ConfigurationManager.GetSection(sectionName) as NameValueCollection;
            var value = section[key];
            
            if (value == null)
            {
                return defaultValue;
            }
            try
            {
                if (typeof(Enum).IsAssignableFrom(typeof(T)))
                    return (T)Enum.Parse(typeof(T), value);
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}
