using Juce.Logic.Graphs;
using Juce.Scripting;
using Juce.Scripting.Instructions;
using Juce.Logic.Attributes;
using System;

namespace Juce.Logic.Nodes
{
    [LogicNode(
        "Bool to String",
        "Conversion",
        new Type[] { typeof(BaseLogicGraph) })
        ]
    public class BoolToStringNode : LogicNode
    {
        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Strict)]
        public bool ValueIn;

        [Output(connectionType = ConnectionType.Multiple, typeConstraint = TypeConstraint.Strict)]
        public string ValueOut;

        protected override void LinkScriptPorts()
        {
            LinkInputPortWithLogicPort(nameof(ValueIn), BoolToStringInstruction.ValueIn, ValueIn);
            LinkOutputPortWithLogicPort(nameof(ValueOut), BoolToStringInstruction.ValueOut);
        }

        protected override ScriptInstruction GenerateInstruction(Script script)
        {
            return script.CreateScriptInstruction<BoolToStringInstruction>();
        }

    }
}
