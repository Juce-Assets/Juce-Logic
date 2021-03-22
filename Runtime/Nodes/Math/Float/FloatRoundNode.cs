using Juce.Logic.Graphs;
using Juce.Scripting;
using Juce.Scripting.Instructions;
using Juce.Logic.Attributes;
using System;

namespace Juce.Logic.Nodes
{
    [LogicNode(
        "Round",
        "Math/Float",
        new Type[] { typeof(BaseLogicGraph) })
        ]
    public class FloatRoundNode : LogicNode
    {
        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Inherited)]
        public float ValueIn;

        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Inherited)]
        public int DecimalsIn;

        [Output(connectionType = ConnectionType.Multiple, typeConstraint = TypeConstraint.None)]
        public float ResultOut;

        protected override void LinkScriptPorts()
        {
            LinkInputPortWithLogicPort(nameof(ValueIn), FloatRoundInstruction.ValueIn, ValueIn);
            LinkInputPortWithLogicPort(nameof(DecimalsIn), FloatRoundInstruction.DecimalsIn, DecimalsIn);
            LinkOutputPortWithLogicPort(nameof(ResultOut), FloatRoundInstruction.ResultOut);
        }

        protected override ScriptInstruction GenerateInstruction(Script script)
        {
            return script.CreateScriptInstruction<FloatRoundInstruction>();
        }
    }
}
