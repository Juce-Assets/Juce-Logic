using Juce.Scripting;
using Juce.Scripting.Instructions;

namespace Juce.Logic.Nodes
{
    public class IntToFloatNode : InstructionNode
    {
        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Strict)]
        public int ValueIn;

        [Output(connectionType = ConnectionType.Multiple, typeConstraint = TypeConstraint.Strict)]
        public float ValueOut;

        protected override void LinkScriptPorts()
        {
            LinkInputPortWithLogicPort(nameof(ValueIn), nameof(IntToFloatInstruction.ValueIn), ValueIn);
            LinkOutputPortWithLogicPort(nameof(ValueOut), nameof(IntToFloatInstruction.ValueOut));
        }

        protected override ScriptInstruction GenerateInstruction(Script script)
        {
            return script.CreateScriptInstruction<IntToFloatInstruction>();
        }

    }
}
