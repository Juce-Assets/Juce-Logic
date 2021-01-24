using Juce.Scripting;
using Juce.Scripting.Instructions;

namespace Juce.OldLogic.Nodes
{
    public class IntToStringNode : LogicNode
    {
        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Strict)]
        public int ValueIn;

        [Output(connectionType = ConnectionType.Multiple, typeConstraint = TypeConstraint.Strict)]
        public string ValueOut;

        protected override void LinkScriptPorts()
        {
            LinkInputPortWithLogicPort(nameof(ValueIn), nameof(IntToStringInstruction.ValueIn), ValueIn);
            LinkOutputPortWithLogicPort(nameof(ValueOut), nameof(IntToStringInstruction.ValueOut));
        }

        protected override ScriptInstruction GenerateInstruction(Script script)
        {
            return script.CreateScriptInstruction<IntToStringInstruction>();
        }

    }
}
