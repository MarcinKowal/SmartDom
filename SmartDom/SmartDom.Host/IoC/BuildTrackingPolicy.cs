using Microsoft.Practices.ObjectBuilder2;
using System.Collections.Generic;

namespace SmartDom.Host.IoC
{
    public interface IBuildTrackingPolicy : IBuilderPolicy
    {
        Stack<object> BuildKeys { get; }
    }

    public class BuildTrackingPolicy : IBuildTrackingPolicy
    {

        public BuildTrackingPolicy()
        {
            BuildKeys = new Stack<object>();
        }

        public Stack<object> BuildKeys { get; private set; }
    }
}