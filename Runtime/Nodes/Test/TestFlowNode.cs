
using Juce.Scripting;

namespace Juce.Logic.Nodes
{
    public class TestFlowNode : FlowNode
    {
        [Output(connectionType = ConnectionType.Override)] public int IntValueOut;

        protected override void LinkScriptPorts()
        {
            LinkOutputPortWithLogicPort(nameof(IntValueOut), nameof(TestFlowInstruction.IntValueOut));
        }

        protected override ScriptInstruction GenerateInstruction(Script script)
        {
            return script.CreateScriptInstruction<TestFlowInstruction>();
        }
    }
}
