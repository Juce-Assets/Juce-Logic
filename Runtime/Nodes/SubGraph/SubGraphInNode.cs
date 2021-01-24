using Juce.Scripting;
using Juce.Scripting.Instructions.SubScript;
using XNode;

namespace Juce.OldLogic.Nodes
{
    public class SubGraphInNode : FlowNode
    {
        protected override ScriptInstruction GenerateInstruction(Script script)
        {
            SubScriptInInstruction subscriptInInstruction = script.CreateScriptInstruction<SubScriptInInstruction>();

            foreach(NodePort nodePort in DynamicOutputs)
            {
                subscriptInInstruction.AddOutputPort(nodePort.ValueType, nodePort.fieldName);
            }

            return subscriptInInstruction;
        }

        protected override void LinkScriptPorts()
        {
            foreach (NodePort nodePort in DynamicOutputs)
            {
                LinkOutputPortWithLogicPort(nodePort.fieldName, nodePort.fieldName);
            }
        }
    }
}
