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
            Script script = CompileAllNodes();

            StartFlowNode startFlowNode = GetNode<StartFlowNode>();

            FlowNode currentFlowNode = startFlowNode;

            while (currentFlowNode != null)
            {
                FlowNode lastFlowNode = currentFlowNode;

                bool found = TryGetNextFlowNode(lastFlowNode, out currentFlowNode);

                if (!found)
                {
                    break;
                }

                LinkFlowNodes(lastFlowNode, currentFlowNode);

                LinkInstructionNode(currentFlowNode);
            }

            return script;
        }

        public bool TryGetNextFlowNode(FlowNode flowNode, out FlowNode nextFlowNode)
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

        public bool TryGetInstructionNodeConnection(NodePort port, out InstructionNode instructionNode)
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

        public Script CompileAllNodes()
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

            return script;
        }

        public void LinkFlowNodes(FlowNode flowNode1, FlowNode flowNode2)
        {
            FlowScriptInstruction flowScriptInstruction1 = flowNode1.CompiledScriptInstruction as FlowScriptInstruction;
            FlowScriptInstruction flowScriptInstruction2 = flowNode2.CompiledScriptInstruction as FlowScriptInstruction;

            flowScriptInstruction1.ConnectFlow(flowScriptInstruction2);
        }

        public void LinkInstructionNode(InstructionNode instructionNode)
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

        public void LinkInputToOutputPort(InstructionNode instructionNode, InstructionNode connectedNode, NodePort inputPort)
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

        public void SetInputPortFallbackValue(InstructionNode instructionNode, NodePort inputPort)
        {
            bool inputLogicPortFound = instructionNode.TryGetInputLogicPort(inputPort, out LogicPort inputLogicPort);

            if(!inputLogicPortFound)
            {
                return;
            }

            instructionNode.CompiledScriptInstruction.SetInputPortFallbackValue(inputLogicPort.Id, inputLogicPort.FallbackValue);
        }

        public T GetNode<T>() where T : Node
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
