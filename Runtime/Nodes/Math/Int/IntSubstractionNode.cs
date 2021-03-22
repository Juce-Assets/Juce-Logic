using Juce.Scripting;
using Juce.Scripting.Instructions;
using Juce.Logic.Attributes;
using Juce.Logic.Graphs;
using System;

namespace Juce.Logic.Nodes
{
    [LogicNode(
        "Substraction",
        "Math/Int",
        new Type[] { typeof(BaseLogicGraph) })
        ]
    public class IntSubstractionNode : LogicNode
    {
        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Inherited)]
        public int ValueAIn;

        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Inherited)]
        public int ValueBIn;

        [Output(connectionType = ConnectionType.Multiple, typeConstraint = TypeConstraint.None)]
        public int ResultOut;

        protected override void LinkScriptPorts()
        {
            LinkInputPortWithLogicPort(nameof(ValueAIn), IntSubstractionInstruction.ValueAIn, ValueAIn);
            LinkInputPortWithLogicPort(nameof(ValueBIn), IntSubstractionInstruction.ValueBIn, ValueBIn);
            LinkOutputPortWithLogicPort(nameof(ResultOut), IntSubstractionInstruction.ResultOut);
        }

        protected override ScriptInstruction GenerateInstruction(Script script)
        {
            return script.CreateScriptInstruction<IntSubstractionInstruction>();
        }
    }
}
