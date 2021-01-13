using Juce.Logic.Nodes;
using Juce.Scripting;
using System;
using XNode;

namespace Juce.Logic.Compiler
{
    public class LogicGraphCompiler
    {
        private readonly LogicGraph logicGraph;

        public LogicGraphCompiler(LogicGraph logicGraph)
        {
            this.logicGraph = logicGraph;
        }

        public Script Compile()
        {
            if(logicGraph == null)
            {
                return null;
            }

            Script script = CompileAllNodes();

            StartFlowNode startFlowNode = GetNode<StartFlowNode>();

            CompileFlow(startFlowNode);

            return script;
        }

        public void CompileFlow(FlowNode flowNode)
        {
            if (logicGraph == null)
            {
                return;
            }

            FlowNode currentFlowNode = flowNode;

            while (currentFlowNode != null)
            {
                LinkInstructionNode(currentFlowNode);

                FlowNode lastFlowNode = currentFlowNode;

                bool found = TryGetNextFlowNode(lastFlowNode, out currentFlowNode);

                if (!found)
                {
                    break;
                }

                LinkFlowNodes(lastFlowNode, currentFlowNode);
            }
        }

        private bool TryGetNextFlowNode(FlowNode flowNode, out FlowNode nextFlowNode)
        {
            NodePort nodePort = flowNode.GetOutputPort(nameof(flowNode.FlowOut));

            if(nodePort.Connection == null)
            {
                nextFlowNode = null;
                return false;
            }

            nextFlowNode = nodePort.Connection.node as FlowNode;

            return true;
        }

        private bool TryGetInstructionNodeConnection(NodePort port, out InstructionNode instructionNode)
        {
            if(port.Connection == null)
            {
                instructionNode = null;
                return false;
            }

            instructionNode = port.Connection.node as InstructionNode;

            if(instructionNode == null)
            {
                return false;
            }

            return true;
        }

        private Script CompileAllNodes()
        {
            Script script = new Script();

            foreach (Node node in logicGraph.nodes)
            {
                InstructionNode instructionNode = node as InstructionNode;

                if (instructionNode == null)
                {
                    continue;
                }

                instructionNode.Compile(script);
            }

            foreach (Node node in logicGraph.nodes)
            {
                InstructionNode instructionNode = node as InstructionNode;

                if (instructionNode == null)
                {
                    continue;
                }

                instructionNode.OnCompile(script);
            }

            return script;
        }

        private void LinkFlowNodes(FlowNode flowNode1, FlowNode flowNode2)
        {
            FlowScriptInstruction flowScriptInstruction1 = flowNode1.CompiledScriptInstruction as FlowScriptInstruction;
            FlowScriptInstruction flowScriptInstruction2 = flowNode2.CompiledScriptInstruction as FlowScriptInstruction;

            flowScriptInstruction1.ConnectFlow(flowScriptInstruction2);
        }

        private void LinkInstructionNode(InstructionNode instructionNode)
        {
            foreach (NodePort inputPort in instructionNode.InputScriptLinks)
            {
                bool found = TryGetInstructionNodeConnection(inputPort, out InstructionNode connectedNode);

                if(!found)
                {
                    SetInputPortFallbackValue(instructionNode, inputPort);

                    continue;
                }

                LinkInstructionNode(connectedNode);

                LinkInputToOutputPort(instructionNode, connectedNode, inputPort);
            }
        }

        private void LinkInputToOutputPort(InstructionNode instructionNode, InstructionNode connectedNode, NodePort inputPort)
        {
            bool inputLogicPortFound = instructionNode.TryGetInputLogicPort(inputPort, out LogicPort inputLogicPort);

            if(!inputLogicPortFound)
            {
                UnityEngine.Debug.LogError("Tried to get input logic port but it was not found");
                return;
            }

            bool outputLogicPortFound = connectedNode.TryGetOutputLogicPort(inputPort.Connection, out LogicPort outputLogicPort);

            if(!outputLogicPortFound)
            {
                UnityEngine.Debug.LogError("Tried to get output logic port but it was not found");
                return;
            }

            instructionNode.CompiledScriptInstruction.ConnectInputToOutputPort(inputLogicPort.Id,
                connectedNode.CompiledScriptInstruction, outputLogicPort.Id);
        }

        private void SetInputPortFallbackValue(InstructionNode instructionNode, NodePort inputPort)
        {
            bool inputLogicPortFound = instructionNode.TryGetInputLogicPort(inputPort, out LogicPort inputLogicPort);

            if(!inputLogicPortFound)
            {
                return;
            }

            instructionNode.CompiledScriptInstruction.SetInputPortFallbackValue(inputLogicPort.Id, inputLogicPort.FallbackValue);
        }

        private T GetNode<T>() where T : Node
        {
            Type lookingType = typeof(T);

            foreach(Node node in logicGraph.nodes)
            {
                if(node.GetType() == lookingType)
                {
                    return node as T;
                }
            }

            return null;
        }
    }
}
