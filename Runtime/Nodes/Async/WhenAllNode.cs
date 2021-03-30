using Juce.Logic.Compiler;
using Juce.Logic.Graphs;
using Juce.Scripting;
using Juce.Scripting.Instructions;
using Juce.Logic.Attributes;
using System;
using XNode;

namespace Juce.Logic.Nodes
{
    [LogicNode(
        "When All",
        "Async",
        new Type[] { typeof(BaseLogicGraph) })
        ]
    public class WhenAllNode : FlowNode
    {
        protected override void LinkScriptPorts()
        {

        }

        protected override ScriptInstruction GenerateInstruction(Script script)
        {
            return script.CreateScriptInstruction<WhenAllInstruction>();
        }

        public override void OnCompile(Script script)
        {
            WhenAllInstruction currentInstruction = CompiledScriptInstruction as WhenAllInstruction;

            foreach (NodePort nodePort in DynamicOutputs)
            {
                if(nodePort.Connection == null)
                {
                    continue;
                }

                FlowNode flowNode = nodePort.Connection.node as FlowNode;

                currentInstruction.AddOutputFlow(flowNode.CompiledScriptInstruction.ScriptInstructionIndex);

                new LogicGraphCompiler(LogicGraph).CompileFlow(flowNode);
            }
        }
    }
}
