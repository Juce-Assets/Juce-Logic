using System.Collections.Generic;
using XNode;

namespace Juce.Logic.Graphs
{
    public class LogicGraph : NodeGraph
    {
        public List<T> GetNodes<T>() where T : LogicNode
        {
            List<T> ret = new List<T>();

            foreach (Node node in nodes)
            {
                if (node.GetType() == typeof(T))
                {
                    if (node is LogicNode)
                    {
                        ret.Add(node as T);
                    }
                }
            }

            return ret;
        }

        public T GetNode<T>() where T : LogicNode
        {
            List<T> nodes = GetNodes<T>();

            if (nodes.Count == 0)
            {
                return null;
            }

            return nodes[0];
        }
    }
}
