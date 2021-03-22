using Juce.Logic.Graphs;
using Juce.Scripting;
using Juce.Scripting.Instructions;
using Juce.Logic.Attributes;
using System;

namespace Juce.Logic.Nodes
{
    [LogicNode(
        "Division",
        "Math/Float",
        new Type[] { typeof(BaseLogicGraph) })
        ]
    public class FloatDivisionNode : LogicNode
    {
        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Inherited)]
        public float ValueAIn;

        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Inherited)]
        public float ValueBIn;

        [Output(connectionType = ConnectionType.Multiple, typeConstraint = TypeConstraint.None)]
        public float ResultOut;

        protected override void LinkScriptPorts()
        {
            LinkInputPortWithLogicPort(nameof(ValueAIn), FloatDivisionInstruction.ValueAIn, ValueAIn);
            LinkInputPortWithLogicPort(nameof(ValueBIn), FloatDivisionInstruction.ValueBIn, ValueBIn);
            LinkOutputPortWithLogicPort(nameof(ResultOut), FloatDivisionInstruction.ResultOut);
        }

        protected override ScriptInstruction GenerateInstruction(Script script)
        {
            return script.CreateScriptInstruction<FloatDivisionInstruction>();
        }
    }
}
