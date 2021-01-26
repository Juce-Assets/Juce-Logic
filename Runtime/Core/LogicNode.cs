using Juce.Logic.Graphs;
using Juce.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using XNode;

namespace Juce.Logic
{
    public abstract class LogicNode : Node
    {
        private readonly Dictionary<NodePort, PortLink> inputScriptLinks = new Dictionary<NodePort, PortLink>();
        private readonly Dictionary<NodePort, PortLink> outputScriptLinks = new Dictionary<NodePort, PortLink>();

        public LogicGraph LogicGraph => graph as LogicGraph;
        public ScriptInstruction CompiledScriptInstruction { get; private set; }

        public List<NodePort> InputScriptLinks => inputScriptLinks.Keys.ToList();

        public override object GetValue(NodePort port)
        {
            return null;
        }

        protected void LinkInputPortWithLogicPort(string graphPortId, string logicPortId, object fallback)
        {
            inputScriptLinks.Add(GetInputPort(graphPortId), new PortLink(logicPortId, fallback));
        }

        protected void LinkOutputPortWithLogicPort(string graphPortId, string logicPortId)
        {
            outputScriptLinks.Add(GetOutputPort(graphPortId), new PortLink(logicPortId, default));
        }

        public bool TryGetInputLogicPort(NodePort nodePort, out PortLink logicPort)
        {
            return inputScriptLinks.TryGetValue(nodePort, out logicPort);
        }

        public bool TryGetOutputLogicPort(NodePort nodePort, out PortLink logicPort)
        {
            return outputScriptLinks.TryGetValue(nodePort, out logicPort);
        }

        public void Compile(Script script)
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
