using Juce.Scripting;
using Juce.Scripting.Instructions;

namespace Juce.Logic.Nodes
{
    public class IntDivisionNode : InstructionNode
    {
        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Strict)]
        public int ValueAIn;

        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Strict)]
        public int ValueBIn;

        [Output(connectionType = ConnectionType.Multiple, typeConstraint = TypeConstraint.Strict)]
        public int ResultOut;

        protected override void LinkScriptPorts()
        {
            LinkInputPortWithLogicPort(nameof(ValueAIn), nameof(IntDivisionInstruction.ValueAIn), ValueAIn);
            LinkInputPortWithLogicPort(nameof(ValueBIn), nameof(IntDivisionInstruction.ValueBIn), ValueBIn);
            LinkOutputPortWithLogicPort(nameof(ResultOut), nameof(IntDivisionInstruction.ResultOut));
        }

        protected override ScriptInstruction GenerateInstruction(Script script)
        {
            return script.CreateScriptInstruction<IntDivisionInstruction>();
        }
    }
}
