using Juce.Logic.Attributes;
using System;

namespace Juce.Logic.Graphs
{
    public static class LogicGraphUtils
    {
        public static bool TryGetLogicNodeAttribute(Type logicNodeType, out LogicNodeAttribute logicNodeAttribute)
        {
            foreach (Attribute attribute in logicNodeType.GetCustomAttributes(typeof(LogicNodeAttribute), false))
            {
                logicNodeAttribute = (LogicNodeAttribute)attribute;
                return true;
            }

            logicNodeAttribute = null;
            return false;
        }

        public static bool LogicNodeCanBeUsedOnGraph(Type logicNodeType, Type logicGraphType)
        {
            bool found = TryGetLogicNodeAttribute(logicNodeType, out LogicNodeAttribute logicNodeAttribute);

            if(!found)
            {
                return false;
            }

            Type[] types = logicNodeAttribute.CanBeUsedOnGraphTypes;

            foreach(Type type in types)
            {
                bool isAssignable = type.IsAssignableFrom(logicGraphType);

                if(isAssignable)
                {
                    return true;
                }
            }

            return false;
        }

        public static string GetLogicNodeFullPath(Type logicNodeType)
        {
            bool found = TryGetLogicNodeAttribute(logicNodeType, out LogicNodeAttribute logicNodeAttribute);

            if (!found)
            {
                return string.Empty;
            }

            if(string.IsNullOrEmpty(logicNodeAttribute.Path))
            {
                return logicNodeAttribute.Name;
            }

            return $"{logicNodeAttribute.Path}/{logicNodeAttribute.Name}";
        }
    }
}
