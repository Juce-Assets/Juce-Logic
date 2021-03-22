using Juce.Logic.Graphs;
using Juce.Logic.Attributes;
using System;

namespace Juce.Logic.Nodes
{
    [LogicNode(
        "Int",
        "Iteration",
        new Type[] { typeof(BaseLogicGraph) })
        ]
    public class ForeachIntNode : ForeachNode<int>
    {
        public ForeachIntNode()
        {

        }
    }
}
