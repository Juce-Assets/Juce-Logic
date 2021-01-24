using Juce.Scripting;
using Juce.Scripting.Instructions;

namespace Juce.OldLogic.Nodes
{
    public class FloatToIntNode : LogicNode
    {
        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Strict)]
        public float ValueIn;

        [Output(connectionType = ConnectionType.Multiple, typeConstraint = TypeConstraint.Strict)]
        public int ValueOut;

        protected override void LinkScriptPorts()
        {
            LinkInputPortWithLogicPort(nameof(ValueIn), nameof(FloatToIntInstruction.ValueIn), ValueIn);
            LinkOutputPortWithLogicPort(nameof(ValueOut), nameof(FloatToIntInstruction.ValueOut));
        }

        protected override ScriptInstruction GenerateInstruction(Script script)
        {
            return script.CreateScriptInstruction<FloatToIntInstruction>();
        }

    }
}
