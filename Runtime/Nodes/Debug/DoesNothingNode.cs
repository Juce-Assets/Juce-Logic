using Juce.Logic.Attributes;
using Juce.Logic.Graphs;
using Juce.Scripting;
using Juce.Scripting.Instructions;
using System;

namespace Juce.Logic.Nodes
{
    [LogicNode(
        "DoesNothing",
        "Debug",
        new Type[] { typeof(BaseLogicGraph) })
        ]
    public class DoesNothingNode : FlowNode
    {
        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Inherited)]
        public int ValueIntIn;

        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Inherited)]
        public float ValueFloatIn;

        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Inherited)]
        public string ValueStringIn;

        [Output(connectionType = ConnectionType.Multiple, typeConstraint = TypeConstraint.None)]
        public int ValueIntOut;

        [Output(connectionType = ConnectionType.Multiple, typeConstraint = TypeConstraint.None)]
        public float ValueFloatOut;

        [Output(connectionType = ConnectionType.Multiple, typeConstraint = TypeConstraint.None)]
        public string ValueStringOut;

        protected override void LinkScriptPorts()
        {
            LinkInputPortWithLogicPort(nameof(ValueIntIn), DoesNothingInstruction.ValueIntIn, ValueIntIn);
            LinkInputPortWithLogicPort(nameof(ValueFloatIn), DoesNothingInstruction.ValueFloatIn, ValueFloatIn);
            LinkInputPortWithLogicPort(nameof(ValueStringIn), DoesNothingInstruction.ValueStringIn, ValueStringIn);

            LinkOutputPortWithLogicPort(nameof(ValueIntOut), DoesNothingInstruction.ValueIntOut);
            LinkOutputPortWithLogicPort(nameof(ValueFloatOut), DoesNothingInstruction.ValueFloatOut);
            LinkOutputPortWithLogicPort(nameof(ValueStringOut), DoesNothingInstruction.ValueStringOut);
        }

        protected override ScriptInstruction GenerateInstruction(Script script)
        {
            return script.CreateScriptInstruction<DoesNothingInstruction>();
        }

    }
}
