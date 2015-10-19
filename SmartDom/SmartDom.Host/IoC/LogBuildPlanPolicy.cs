using Microsoft.Practices.ObjectBuilder2;

using ServiceStack.Logging;
using System;


namespace SmartDom.Host.IoC
{
    public class LogBuildPlanPolicy : IBuildPlanPolicy
    {

        public LogBuildPlanPolicy(Type logType)
        {
            LogType = logType;
        }

        public Type LogType { get; private set; }

        public void BuildUp(IBuilderContext context)
        {
            if (context.Existing == null)
            {
                ILog log = LogManager.GetLogger(LogType);
                context.Existing = log;
            }
        }
    }
}
