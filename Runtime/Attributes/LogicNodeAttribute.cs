using System;

namespace Juce.Logic.Attributes
{
    public class LogicNodeAttribute : Attribute
    {
        public string Name { get; }
        public string Path { get; }
        public Type[] CanBeUsedOnGraphTypes { get; }

        public LogicNodeAttribute(
            string name, 
            string path, 
            Type[] canBeUsedOnGraphTypes
            )
        {
            Name = name;
            Path = path;
            CanBeUsedOnGraphTypes = canBeUsedOnGraphTypes;
        }
    }
}
