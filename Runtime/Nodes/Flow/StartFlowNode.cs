
using Juce.Scripting;

namespace Juce.OldLogic.Nodes
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
