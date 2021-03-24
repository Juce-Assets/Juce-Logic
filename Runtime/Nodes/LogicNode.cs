using Juce.Logic.Graphs;
using Juce.Scripting;
using Juce.Scripting.Instructions;
using System.Collections.Generic;
using System.Linq;
using XNode;

namespace Juce.Logic.Nodes
{
    public abstract class LogicNode : Node
    {
        private readonly Dictionary<NodePort, PortLink> inputScriptLinks = new Dictionary<NodePort, PortLink>();
        private readonly Dictionary<NodePort, PortLink> outputScriptLinks = new Dictionary<NodePort, PortLink>();

        public BaseLogicGraph LogicGraph => graph as BaseLogicGraph;
        public ScriptInstruction CompiledScriptInstruction { get; private set; }

        public List<NodePort> InputScriptLinks => inputScriptLinks.Keys.ToList();

        public sealed override object GetValue(NodePort port)
        {
            return null;
        }

        protected void LinkInputPortWithLogicPort(string graphPortId, string logicPortId, object fallback)
        {
            NodePort nodePort = GetInputPort(graphPortId);

            if(nodePort == null)
            {
                throw new System.Exception($"Input Node port with id {graphPortId} could not be found");
            }

            inputScriptLinks.Add(nodePort, new PortLink(logicPortId, fallback));
        }

        protected void LinkOutputPortWithLogicPort(string graphPortId, string logicPortId)
        {
            NodePort nodePort = GetOutputPort(graphPortId);

            if (nodePort == null)
            {
                throw new System.Exception($"Output Node port with id {graphPortId} could not be found");
            }

            outputScriptLinks.Add(nodePort, new PortLink(logicPortId, default));
        }

        public bool TryGetInputLogicPort(NodePort nodePort, out PortLink logicPort)
        {
            return inputScriptLinks.TryGetValue(nodePort, out logicPort);
        }

        public bool TryGetOutputLogicPort(NodePort nodePort, out PortLink logicPort)
        {
            return outputScriptLinks.TryGetValue(nodePort, out logicPort);
        }

        public void OnPreCompile(Script script)
        {
            inputScriptLinks.Clear();
            outputScriptLinks.Clear();
            CompiledScriptInstruction = null;

            LinkScriptPorts();
            CompiledScriptInstruction = GenerateInstruction(script);
        }

        protected abstract void LinkScriptPorts();
        protected abstract ScriptInstruction GenerateInstruction(Script script);
        public virtual void OnCompile(Script script) { }
    }
}
