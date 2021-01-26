using System;

namespace Juce.Logic
{
    public class PortLink
    {
        public string Id { get; }
        public object FallbackValue { get; }

        public PortLink(string id, object defaultValue)
        {
            Id = id;
            FallbackValue = defaultValue;
        }
    }
}
