using Juce.Scripting;
using Juce.Scripting.Instructions;

namespace Juce.Logic.Nodes
{
    public class IntToFloatNode : LogicNode
    {
        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Inherited)]
        public int ValueIn;

        [Output(connectionType = ConnectionType.Multiple, typeConstraint = TypeConstraint.Inherited)]
        public float ValueOut;

        protected override void LinkScriptPorts()
        {
            LinkInputPortWithLogicPort(nameof(ValueIn), IntToFloatInstruction.ValueIn, ValueIn);
            LinkOutputPortWithLogicPort(nameof(ValueOut), IntToFloatInstruction.ValueOut);
        }

        protected override ScriptInstruction GenerateInstruction(Script script)
        {
            return script.CreateScriptInstruction<IntToFloatInstruction>();
        }

    }
}
