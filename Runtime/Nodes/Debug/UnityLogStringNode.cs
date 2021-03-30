using Juce.Logic.Attributes;
using Juce.Logic.Graphs;
using Juce.Scripting;
using Juce.Scripting.Instructions;
using System;

namespace Juce.Logic.Nodes
{
    [LogicNode(
        "Log",
        "Debug",
        new Type[] { typeof(BaseLogicUnityGraph) })
        ]
    public class UnityLogStringNode : FlowNode
    {
        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.None)]
        public string ValueIn;

        protected override void LinkScriptPorts()
        {
            LinkInputPortWithLogicPort(nameof(ValueIn), UnityLogInstruction.ValueIn, ValueIn);
        }

        protected override ScriptInstruction GenerateInstruction(Script script)
        {
            return script.CreateScriptInstruction<UnityLogInstruction>();
        }
    }
}
