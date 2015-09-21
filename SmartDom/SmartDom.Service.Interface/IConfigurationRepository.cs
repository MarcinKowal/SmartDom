//
// SmartDom
// SmartDom.Service.Interface
// IConfigurationRepository.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
// 

namespace SmartDom.Service.Interface
{
    public interface IConfigurationRepository
    {
        T GetConfigurationValue<T>(string sectionName, string key);
        T GetConfigurationValue<T>(string sectionName, string key, T defaultValue);
    }
}
