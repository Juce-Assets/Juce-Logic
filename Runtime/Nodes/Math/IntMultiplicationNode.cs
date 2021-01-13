using Juce.Scripting;
using Juce.Scripting.Instructions;

namespace Juce.Logic.Nodes
{
    public class IntMultiplicationNode : InstructionNode
    {
        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Strict)]
        public int ValueAIn;

        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Strict)]
        public int ValueBIn;

        [Output(connectionType = ConnectionType.Multiple, typeConstraint = TypeConstraint.Strict)]
        public int ResultOut;

        protected override void LinkScriptPorts()
        {
            LinkInputPortWithLogicPort(nameof(ValueAIn), nameof(IntMultiplicationInstruction.ValueAIn), ValueAIn);
            LinkInputPortWithLogicPort(nameof(ValueBIn), nameof(IntMultiplicationInstruction.ValueBIn), ValueBIn);
            LinkOutputPortWithLogicPort(nameof(ResultOut), nameof(IntMultiplicationInstruction.ResultOut));
        }

        protected override ScriptInstruction GenerateInstruction(Script script)
        {
            return script.CreateScriptInstruction<IntMultiplicationInstruction>();
        }
    }
}
