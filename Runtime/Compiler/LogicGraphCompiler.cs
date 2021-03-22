using Juce.Logic.Graphs;
using Juce.Logic.Nodes;
using Juce.Scripting;
using Juce.Scripting.Instructions;
using System.Collections.Generic;
using XNode;

namespace Juce.Logic.Compiler
{
    public class LogicGraphCompiler
    {
        private readonly BaseLogicGraph logicGraph;

        private List<LogicNode> compiledNodes = new List<LogicNode>();

        public LogicGraphCompiler(BaseLogicGraph logicGraph)
        {
            this.logicGraph = logicGraph;
        }

        public Script CompileFromStartFlowNode()
        {
            if(logicGraph == null)
            {
                return null;
            }

            StartFlowNode startFlowNode = logicGraph.GetNode<StartFlowNode>();

            Script script = CompileAllNodes();

            CompileFlow(startFlowNode);

            return script;
        }

        public Script CompileFromSubgraphInNode(Script parentScript)
        {
            if (logicGraph == null)
            {
                return null;
            }

            SubGraphInNode subGraphInNode = logicGraph.GetNode<SubGraphInNode>();

            Script script = CompileAllNodes(parentScript);

            CompileFlow(subGraphInNode);

            return script;
        }

        public void CompileFlow(FlowNode flowNode)
        {
            if (logicGraph == null)
            {
                return;
            }

            compiledNodes.Clear();

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

        private bool TryGetInstructionNodeConnection(NodePort port, out LogicNode instructionNode)
        {
            if(port.Connection == null)
            {
                instructionNode = null;
                return false;
            }

            instructionNode = port.Connection.node as LogicNode;

            if(instructionNode == null)
            {
                return false;
            }

            return true;
        }

        private Script CompileAllNodes(Script parentScript = null)
        {
            Script script;

            if (parentScript == null)
            {
                script = new Script();
            }
            else
            {
                script = parentScript.AddSubScript();
            }

            foreach (Node node in logicGraph.nodes)
            {
                LogicNode instructionNode = node as LogicNode;

                if (instructionNode == null)
                {
                    continue;
                }

                instructionNode.OnPreCompile(script);
            }

            foreach (Node node in logicGraph.nodes)
            {
                LogicNode instructionNode = node as LogicNode;

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
            FlowInstruction flowScriptInstruction1 = flowNode1.CompiledScriptInstruction as FlowInstruction;
            FlowInstruction flowScriptInstruction2 = flowNode2.CompiledScriptInstruction as FlowInstruction;

            flowScriptInstruction1.ConnectFlow(flowScriptInstruction2);
        }

        private void LinkInstructionNode(LogicNode instructionNode)
        {
            if (compiledNodes.Contains(instructionNode))
            {
                return;
            }

            compiledNodes.Add(instructionNode);

            foreach (NodePort inputPort in instructionNode.InputScriptLinks)
            {
                bool found = TryGetInstructionNodeConnection(inputPort, out LogicNode connectedNode);

                if (!found)
                {
                    SetInputPortFallbackValue(instructionNode, inputPort);

                    continue;
                }

                LinkInstructionNode(connectedNode);

                LinkInputToOutputPort(instructionNode, connectedNode, inputPort);
            }
        }

        private void LinkInputToOutputPort(LogicNode instructionNode, LogicNode connectedNode, NodePort inputPort)
        {
            bool inputLogicPortFound = instructionNode.TryGetInputLogicPort(inputPort, out PortLink inputLogicPort);

            if(!inputLogicPortFound)
            {
                UnityEngine.Debug.LogError("Tried to get input logic port but it was not found");
                return;
            }

            bool outputLogicPortFound = connectedNode.TryGetOutputLogicPort(inputPort.Connection, out PortLink outputLogicPort);

            if(!outputLogicPortFound)
            {
                UnityEngine.Debug.LogError("Tried to get output logic port but it was not found");
                return;
            }

            instructionNode.CompiledScriptInstruction.ConnectInputToOutputPort(inputLogicPort.Id,
                connectedNode.CompiledScriptInstruction, outputLogicPort.Id);
        }

        private void SetInputPortFallbackValue(LogicNode instructionNode, NodePort inputPort)
        {
            bool inputLogicPortFound = instructionNode.TryGetInputLogicPort(inputPort, out PortLink inputLogicPort);

            if(!inputLogicPortFound)
            {
                return;
            }

            instructionNode.CompiledScriptInstruction.SetInputPortFallbackValue(inputLogicPort.Id, inputLogicPort.FallbackValue);
        }
    }
}
