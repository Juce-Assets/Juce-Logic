using Juce.Logic.Nodes;
using System;
using System.Collections.Generic;
using XNode;

namespace Juce.Logic.Graphs
{
    public class BaseLogicGraph : NodeGraph
    {
        public List<T> GetNodes<T>() where T : LogicNode
        {
            List<T> ret = new List<T>();

            Type type = typeof(T);

            foreach (Node node in nodes)
            {
                if (node.GetType() != type)
                {
                    continue;
                }

                ret.Add((T)node);
            }

            return ret;
        }

        public T GetNode<T>() where T : LogicNode
        {
            TrGetNode(out T node);

            return node;
        }

        public bool TrGetNode<T>(out T node) where T : LogicNode
        {
            List<T> nodes = GetNodes<T>();

            if (nodes.Count == 0)
            {
                node = null;
                return false;
            }

            node = nodes[0];
            return true;
        }
    }
}
