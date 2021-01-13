
using Juce.Scripting;

namespace Juce.Logic.Nodes
{
    public class Test2FlowNode : FlowNode
    {
        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Strict)] 
        public int IntValueIn;

        [Output(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Strict)] 
        public int IntValueOut;

        protected override void LinkScriptPorts()
        {
            LinkInputPortWithLogicPort(nameof(IntValueIn), nameof(Test2FlowInstruction.IntValueIn), IntValueIn);
            LinkOutputPortWithLogicPort(nameof(IntValueOut), nameof(Test2FlowInstruction.IntValueOut));
        }

        protected override ScriptInstruction GenerateInstruction(Script script)
        {
            return script.CreateScriptInstruction<Test2FlowInstruction>();
        }

    }
}
