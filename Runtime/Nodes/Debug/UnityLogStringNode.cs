using Juce.Scripting;
using Juce.Scripting.Instructions;

namespace Juce.OldLogic.Nodes
{
    public class UnityLogStringNode : FlowNode
    {
        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Strict)]
        public string ValueIn;

        protected override void LinkScriptPorts()
        {
            LinkInputPortWithLogicPort(nameof(ValueIn), nameof(UnityLogInstruction.ValueIn), ValueIn);
        }

        protected override ScriptInstruction GenerateInstruction(Script script)
        {
            return script.CreateScriptInstruction<UnityLogInstruction>();
        }

    }
}
