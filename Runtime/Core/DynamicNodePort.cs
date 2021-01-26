using XNode;

namespace Juce.Logic
{
    [System.Serializable]
    public class DynamicNodePort 
    {
        public NodePort NodePort { get; }
        public string Name { get; set; }

        public DynamicNodePort(NodePort nodePort)
        {
            NodePort = nodePort;
        }
    }
}
