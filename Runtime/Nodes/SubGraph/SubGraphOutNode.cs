using Juce.Scripting;
using Juce.Scripting.Instructions;
using Juce.Scripting.Instructions.SubScript;
using XNode;

namespace Juce.Logic.Nodes
{
    public class SubGraphOutNode : FlowNode
    {
        protected override ScriptInstruction GenerateInstruction(Script script)
        {
            SubScriptOutInstruction subscriptInInstruction = script.CreateScriptInstruction<SubScriptOutInstruction>();

            foreach (NodePort nodePort in DynamicInputs)
            {
                subscriptInInstruction.AddInputPort(nodePort.ValueType, nodePort.fieldName);
            }

            return subscriptInInstruction;
        }

        protected override void LinkScriptPorts()
        {
            foreach (NodePort nodePort in DynamicInputs)
            {
                LinkInputPortWithLogicPort(nodePort.fieldName, nodePort.fieldName, default);
            }
        }
    }
}
