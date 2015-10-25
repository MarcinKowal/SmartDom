// SmartDom
// SmartDom.Host
// BuildTracking.cs
//  
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
//  

namespace SmartDom.Host.IoC
{
    using Microsoft.Practices.ObjectBuilder2;
    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.ObjectBuilder;

    /// <summary>
    /// </summary>
    public class BuildTracking : UnityContainerExtension
    {
        /// <summary>
        /// </summary>
        protected override void Initialize()
        {
            this.Context.Strategies.AddNew<BuildTrackingStrategy>(UnityBuildStage.TypeMapping);
        }

        /// <summary>
        /// </summary>
        /// <param name="context">
        /// </param>
        /// <returns>
        /// </returns>
        public static IBuildTrackingPolicy GetPolicy(IBuilderContext context)
        {
            return context.Policies.Get<IBuildTrackingPolicy>(context.BuildKey, true);
        }

        /// <summary>
        /// </summary>
        /// <param name="context">
        /// </param>
        /// <returns>
        /// </returns>
        public static IBuildTrackingPolicy SetPolicy(IBuilderContext context)
        {
            IBuildTrackingPolicy policy = new BuildTrackingPolicy();
            context.Policies.SetDefault(policy);
            return policy;
        }
    }
}