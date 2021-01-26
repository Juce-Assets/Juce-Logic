
using Juce.Scripting;

namespace Juce.Logic.Nodes
{
    public class StartFlowNode : FlowNode
    {
        protected override ScriptInstruction GenerateInstruction(Script script)
        {
            return script.CreateScriptInstruction<StartFlowInstruction>();
        }

        protected override void LinkScriptPorts()
        {
          
        }
    }
}
