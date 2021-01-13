using System;

namespace Juce.Logic
{
    public class LogicPort
    {
        public string Id { get; }
        public object FallbackValue { get; }

        public LogicPort(string id, object defaultValue)
        {
            Id = id;
            FallbackValue = defaultValue;
        }
    }
}
