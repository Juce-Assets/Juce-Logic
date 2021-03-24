using System;
using XNodeEditor;

namespace Juce.Logic.Graphs
{
    [CustomNodeGraphEditor(typeof(LogicSubGraph))]
    public class LogicSubGraphEditor : NodeGraphEditor
    {
        private LogicSubGraph Graph => target as LogicSubGraph;

        public override string GetNodeMenuName(Type type)
        {
            bool canBeUsed = LogicGraphUtils.LogicNodeCanBeUsedOnGraph(type, Graph.GetType());

            if (!canBeUsed)
            {
                return null;
            }
           return LogicGraphUtils.GetLogicNodeFullPath(type);
        }
    }
}
