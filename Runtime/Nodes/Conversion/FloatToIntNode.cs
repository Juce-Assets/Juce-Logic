using Juce.Logic.Graphs;
using Juce.Scripting;
using Juce.Scripting.Instructions;
using Juce.Logic.Attributes;
using System;

namespace Juce.Logic.Nodes
{
    [LogicNode(
        "Float to Int",
        "Conversion",
        new Type[] { typeof(BaseLogicGraph) })
        ]
    public class FloatToIntNode : LogicNode
    {
        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Strict)]
        public float ValueIn;

        [Output(connectionType = ConnectionType.Multiple, typeConstraint = TypeConstraint.Strict)]
        public int ValueOut;

        protected override void LinkScriptPorts()
        {
            LinkInputPortWithLogicPort(nameof(ValueIn), FloatToIntInstruction.ValueIn, ValueIn);
            LinkOutputPortWithLogicPort(nameof(ValueOut), FloatToIntInstruction.ValueOut);
        }

        protected override ScriptInstruction GenerateInstruction(Script script)
        {
            return script.CreateScriptInstruction<FloatToIntInstruction>();
        }

    }
}
