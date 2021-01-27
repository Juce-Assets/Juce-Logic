using Juce.Scripting;
using Juce.Scripting.Instructions;

namespace Juce.Logic.Nodes
{
    public class UnityLogStringNode : FlowNode
    {
        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.None)]
        public string ValueIn;

        protected override void LinkScriptPorts()
        {
            LinkInputPortWithLogicPort(nameof(ValueIn), UnityLogInstruction.ValueIn, ValueIn);
        }

        protected override ScriptInstruction GenerateInstruction(Script script)
        {
            return script.CreateScriptInstruction<UnityLogInstruction>();
        }
    }
}
