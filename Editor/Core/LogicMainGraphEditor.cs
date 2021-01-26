using Juce.Logic.Nodes;
using System;
using XNode;
using XNodeEditor;

namespace Juce.Logic.Graphs
{
    [CustomNodeGraphEditor(typeof(LogicMainGraph))]
    public class LogicMainGraphEditor : NodeGraphEditor
    {
        private LogicMainGraph Graph => target as LogicMainGraph;

        public override string GetNodeMenuName(Type type)
        {
            if (typeof(StartFlowNode).IsAssignableFrom(type))
            {
                return null;
            }

            if (typeof(SubGraphInNode).IsAssignableFrom(type))
            {
                return null;
            }

            if (typeof(SubGraphOutNode).IsAssignableFrom(type))
            {
                return null;
            }

            return base.GetNodeMenuName(type);
        }
    }
}
