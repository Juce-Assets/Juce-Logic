using System;
using XNodeEditor;

namespace Juce.Logic.Graphs
{
    [CustomNodeGraphEditor(typeof(LogicMainGraph))]
    public class LogicMainGraphEditor : NodeGraphEditor
    {
        private LogicMainGraph Graph => target as LogicMainGraph;

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
