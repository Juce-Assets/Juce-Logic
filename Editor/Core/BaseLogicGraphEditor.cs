using System;
using XNodeEditor;

namespace Juce.Logic.Graphs
{
    [CustomNodeGraphEditor(typeof(BaseLogicGraph))]
    public class BaseLogicGraphEditor : NodeGraphEditor
    {
        private BaseLogicGraph Graph => target as BaseLogicGraph;

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
