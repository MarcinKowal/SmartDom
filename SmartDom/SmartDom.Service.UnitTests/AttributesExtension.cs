using ServiceStack;
using System;

namespace SmartDom.Service.UnitTests
{
    public static class AttributesExtension
    {
        public static bool HasRoutingAttribute(Type type,string expectedHttpVerb, string expectedHttpRoutingPath)
        {
            var att = Attribute.GetCustomAttribute(type, typeof (RouteAttribute)) as RouteAttribute;
            if (null == att)
                return false;
            if (!att.Verbs.Equals(expectedHttpVerb,StringComparison.InvariantCultureIgnoreCase))
                return false;
            return !att.Path.Equals(expectedHttpRoutingPath,StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
