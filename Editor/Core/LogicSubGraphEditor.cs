using Juce.OldLogic.Nodes;
using System;
using XNode;
using XNodeEditor;

namespace Juce.OldLogic.Graphs
{
    [CustomNodeGraphEditor(typeof(LogicSubGraph))]
    public class LogicSubGraphEditor : NodeGraphEditor
    {
        private LogicSubGraph Graph => target as LogicSubGraph;

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
