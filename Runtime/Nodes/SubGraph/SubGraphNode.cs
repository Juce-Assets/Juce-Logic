using Juce.Logic.Compiler;
using Juce.Logic.Graphs;
using Juce.Scripting;
using Juce.Scripting.Instructions.SubScript;
using UnityEngine;
using XNode;

namespace Juce.Logic.Nodes
{
    public class SubGraphNode : FlowNode
    {
        [SerializeField] [HideInInspector] private LogicSubGraph logicSubGraph;

        public override void OnCompile(Script script)
        {
            if (logicSubGraph == null)
            {
                return;
            }

            SubGraphInNode subGraphInFlowNode = logicSubGraph.GetNode<SubGraphInNode>();
            SubGraphOutNode subGraphOutFlowNode = logicSubGraph.GetNode<SubGraphOutNode>();

            if (subGraphInFlowNode == null || subGraphOutFlowNode == null)
            {
                return;
            }

            Script subscript = new LogicGraphCompiler(logicSubGraph, script).CompileFlow(subGraphInFlowNode);

            SubScriptInstruction currentInstruction = CompiledScriptInstruction as SubScriptInstruction;
            SubScriptInInstruction subscriptInInstruction = subGraphInFlowNode.CompiledScriptInstruction as SubScriptInInstruction;
            SubScriptOutInstruction subscriptOutInstruction = subGraphOutFlowNode.CompiledScriptInstruction as SubScriptOutInstruction;

            currentInstruction.SubScriptIndex = subscript.SubScriptIndex;
            currentInstruction.SubscriptInInstructionIndex = subscriptInInstruction.ScriptInstructionIndex;
            currentInstruction.SubscriptOutInstructionIndex = subscriptOutInstruction.ScriptInstructionIndex;
        }

        protected override ScriptInstruction GenerateInstruction(Script script)
        {
            SubScriptInstruction subscriptInstruction = script.CreateScriptInstruction<SubScriptInstruction>();

            foreach (NodePort nodePort in DynamicInputs)
            {
                subscriptInstruction.AddInputPort(nodePort.ValueType, nodePort.fieldName);
            }

            foreach (NodePort nodePort in DynamicOutputs)
            {
                subscriptInstruction.AddOutputPort(nodePort.ValueType, nodePort.fieldName);
            }

            return subscriptInstruction;
        }

        protected override void LinkScriptPorts()
        {
            foreach (NodePort nodePort in DynamicInputs)
            {
                LinkInputPortWithLogicPort(nodePort.fieldName, nodePort.fieldName, default);
            }

            foreach (NodePort nodePort in DynamicOutputs)
            {
                LinkOutputPortWithLogicPort(nodePort.fieldName, nodePort.fieldName);
            }
        }
    }
}
